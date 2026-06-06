using Microsoft.EntityFrameworkCore;
using SalaryManagement.Application.DTOs;
using SalaryManagement.Application.Interfaces;
using SalaryManagement.Domain.Entities;
using SalaryManagement.Domain.Enums;
using SalaryManagement.Infrastructure.Data;

namespace SalaryManagement.Application.Services
{
    public class BangLuongService : IBangLuongService
    {
        private readonly AppDbContext _context;
        private readonly ITinhThueService _tinhThueService;
        private readonly IBaoHiemService _baoHiemService;

        // Lương tối thiểu vùng 2026 (Nghị định 293/2025/NĐ-CP)
        private static readonly Dictionary<int, decimal> LuongToiThieuVung = new()
        {
            { 1, 5_310_000m },
            { 2, 4_730_000m },
            { 3, 4_140_000m },
            { 4, 3_700_000m },
        };

        public BangLuongService(AppDbContext context, ITinhThueService tinhThueService, IBaoHiemService baoHiemService)
        {
            _context = context;
            _tinhThueService = tinhThueService;
            _baoHiemService = baoHiemService;
        }

        // 1. Tạo bảng lương cho 1 nhân viên
        public async Task<BangLuong> TaoBangLuongAsync(int nhanVienId, int thang, int nam, decimal soNgayCongThucTe, decimal soGioLamThem = 0)
        {
            var nhanVien = await _context.NhanViens
                .Include(e => e.CauHinhLuong)
                .Include(e => e.PhongBan)
                .Include(e => e.ChucVu)
                .FirstOrDefaultAsync(e => e.Id == nhanVienId)
                ?? throw new InvalidOperationException($"Không tìm thấy nhân viên ID: {nhanVienId}");

            var cauHinh = nhanVien.CauHinhLuong
                ?? throw new InvalidOperationException($"Nhân viên {nhanVien.HoTen} chưa có cấu hình lương");

            // Lấy cấu hình hệ thống mới nhất
            var cauHinhHeThong = await _context.CauHinhHeThongs
                .OrderByDescending(c => c.NgayHieuLuc)
                .FirstOrDefaultAsync()
                ?? throw new InvalidOperationException("Chưa có cấu hình hệ thống!");

            // Kiểm tra lương tối thiểu vùng 2026
            if (!LuongToiThieuVung.TryGetValue(nhanVien.Vung, out var luongToiThieu))
                throw new InvalidOperationException($"Nhân viên {nhanVien.HoTen} chưa được gán vùng lương hợp lệ (1-4)");

            if (cauHinh.LuongCoBan < luongToiThieu)
                throw new InvalidOperationException(
                    $"Lương cơ bản ({cauHinh.LuongCoBan:N0}đ) thấp hơn lương tối thiểu vùng {nhanVien.Vung} ({luongToiThieu:N0}đ)");

            // Xóa bảng lương cũ nếu tồn tại (chưa duyệt)
            var bangLuongCu = await _context.BangLuongs
                .FirstOrDefaultAsync(p => p.NhanVienId == nhanVienId && p.Thang == thang && p.Nam == nam);
            if (bangLuongCu != null)
            {
                if (bangLuongCu.TrangThai == TrangThaiBangLuong.DaDuyet || bangLuongCu.TrangThai == TrangThaiBangLuong.DaTra)
                    throw new InvalidOperationException("Bảng lương đã được duyệt, không thể tính lại");
                _context.BangLuongs.Remove(bangLuongCu);
            }

            // Lấy danh sách thưởng/phạt trong tháng
            var dsThuongPhat = await _context.ThuongPhats
                .Where(bp => bp.NhanVienId == nhanVienId && bp.Thang == thang && bp.Nam == nam)
                .ToListAsync();

            var tongThuong = dsThuongPhat
                .Where(bp => bp.Loai != LoaiThuongPhat.PhatViPham && bp.Loai != LoaiThuongPhat.KhauTruKhac)
                .Sum(bp => bp.SoTien);
            var tongPhat = dsThuongPhat
                .Where(bp => bp.Loai == LoaiThuongPhat.PhatViPham || bp.Loai == LoaiThuongPhat.KhauTruKhac)
                .Sum(bp => Math.Abs(bp.SoTien));

            // Lương theo ngày công
            decimal luongGio = cauHinh.LuongCoBan / cauHinh.SoNgayCongChuan / 8;
            decimal luongTheoNgay = (cauHinh.LuongCoBan / cauHinh.SoNgayCongChuan) * soNgayCongThucTe;

            // Lương làm thêm giờ (150%) - dùng để tính lương thực nhận
            decimal luongLamThem = soGioLamThem * luongGio * 1.5m;

            // Phần làm thêm chịu thuế (chỉ 100%, phần 50% chênh lệch được miễn theo luật TNCN 2025)
            decimal luongLamThemChiuThue = soGioLamThem * luongGio * 1.0m;

            // Phụ cấp theo ngày công (từ cấu hình hệ thống)
            decimal phuCapAnTrua = cauHinhHeThong.PhuCapAnTruaMoiNgay * soNgayCongThucTe;
            decimal phuCapXangXe = cauHinhHeThong.PhuCapXangXeMoiNgay * soNgayCongThucTe;

            // Phụ cấp cố định (từ chức vụ)
            decimal tongPhuCap = phuCapAnTrua + phuCapXangXe;
            // Gross = toàn bộ thu nhập thực nhận (bao gồm 150% OT)
            decimal gross = luongTheoNgay + tongPhuCap + tongThuong - tongPhat + luongLamThem;

            // Thu nhập tính thuế = chỉ tính 100% OT, loại phần 50% chênh lệch miễn thuế
            decimal thuNhapTinhThue = luongTheoNgay + tongPhuCap + tongThuong - tongPhat + luongLamThemChiuThue;

            // Bảo hiểm nhân viên đóng
            decimal bhXh = _baoHiemService.TinhBaoHiemXaHoiNhanVien(cauHinh.LuongCoBan);
            decimal bhYt = _baoHiemService.TinhBaoHiemYTeNhanVien(cauHinh.LuongCoBan);
            decimal bhTn = _baoHiemService.TinhBaoHiemThatNghiepNhanVien(cauHinh.LuongCoBan);
            decimal tongBaoHiem = bhXh + bhYt + bhTn;

            // Giảm trừ gia cảnh
            decimal giamTruBanThan = _tinhThueService.LayGiamTruBanThan();
            decimal giamTruNguoiPhuThuoc = nhanVien.SoNguoiPhuThuoc * _tinhThueService.LayGiamTruNguoiPhuThuoc();

            // Tính thuế TNCN trên thu nhập tính thuế (không gồm 50% OT miễn thuế)
            decimal thuNhapChiuThue = _tinhThueService.TinhThuNhapChiuThue(thuNhapTinhThue, tongBaoHiem, nhanVien.SoNguoiPhuThuoc, nhanVien.LaCuTru);
            decimal thueTncn = _tinhThueService.TinhThueThuNhapCaNhan(thuNhapChiuThue);

            // Net = Gross thực nhận - bảo hiểm - thuế
            decimal net = gross - tongBaoHiem - thueTncn;

            // Chi phí công ty đóng
            decimal bhXhCt = _baoHiemService.TinhBaoHiemXaHoiCongTy(cauHinh.LuongCoBan);
            decimal bhYtCt = _baoHiemService.TinhBaoHiemYTeCongTy(cauHinh.LuongCoBan);
            decimal bhTnCt = _baoHiemService.TinhBaoHiemThatNghiepCongTy(cauHinh.LuongCoBan);
            decimal tongChiPhiCty = gross + bhXhCt + bhYtCt + bhTnCt;

            var bangLuong = new BangLuong
            {
                NhanVienId = nhanVienId,
                Thang = thang,
                Nam = nam,
                SoNgayCongChuan = cauHinh.SoNgayCongChuan,
                SoNgayCongThucTe = soNgayCongThucTe,
                SoGioLamThem = soGioLamThem,
                LuongCoBan = cauHinh.LuongCoBan,
                PhuCapAnTrua = phuCapAnTrua,
                PhuCapXangXe = phuCapXangXe,
                PhuCapTrachNhiem = 0,
                PhuCapDienThoai = 0,
                PhuCapNhaO = 0,
                LuongTheoNgayCong = Math.Round(luongTheoNgay, 0),
                LuongLamThem = Math.Round(luongLamThem, 0),
                TongThuong = Math.Round(tongThuong, 0),
                TongPhat = Math.Round(tongPhat, 0),
                TongThuNhap = Math.Round(gross, 0),
                BaoHiemXaHoi = bhXh,
                BaoHiemYTe = bhYt,
                BaoHiemThatNghiep = bhTn,
                ThuNhapChiuThue = Math.Round(thuNhapChiuThue, 0),
                GiamTruBanThan = giamTruBanThan,
                GiamTruNguoiPhuThuoc = giamTruNguoiPhuThuoc,
                ThueThuNhapCaNhan = thueTncn,
                ThuNhapRong = Math.Round(net, 0),
                BhXhCongTy = bhXhCt,
                BhYtCongTy = bhYtCt,
                BhTnCongTy = bhTnCt,
                TongChiPhiCongTy = Math.Round(tongChiPhiCty, 0),
                TrangThai = TrangThaiBangLuong.Nhap
            };

            _context.BangLuongs.Add(bangLuong);
            await _context.SaveChangesAsync();
            return bangLuong;
        }

        // 2. Tạo bảng lương hàng loạt cho tất cả nhân viên đang làm việc
        public async Task<List<BangLuong>> TaoBangLuongHangThangAsync(int thang, int nam)
        {
            var nhanViens = await _context.NhanViens
                .Include(e => e.CauHinhLuong)
                .Where(e => e.TrangThai == TrangThaiNhanVien.DangLamViec && e.CauHinhLuong != null)
                .ToListAsync();

            var dsChamCong = await _context.ChamCongs
                .Where(c => c.Thang == thang && c.Nam == nam)
                .ToListAsync();

            var danhSach = new List<BangLuong>();
            foreach (var nv in nhanViens)
            {
                var chamCong = dsChamCong.FirstOrDefault(c => c.NhanVienId == nv.Id);

                var soNgayCong = chamCong != null
                    ? (decimal)chamCong.SoNgayLamViec
                    : nv.CauHinhLuong!.SoNgayCongChuan;

                var soGioTangCa = chamCong != null
                    ? chamCong.SoGioTangCa
                    : 0;

                var bangLuong = await TaoBangLuongAsync(nv.Id, thang, nam, soNgayCong, soGioTangCa);
                danhSach.Add(bangLuong);
            }
            return danhSach;
        }

        // 3. Lấy bảng lương của một nhân viên (DTO)
        public async Task<BangLuongDto?> LayBangLuongAsync(int nhanVienId, int thang, int nam)
        {
            var bangLuong = await _context.BangLuongs
                .Include(p => p.NhanVien).ThenInclude(e => e.PhongBan)
                .Include(p => p.NhanVien).ThenInclude(e => e.ChucVu)
                .Include(p => p.ThuongPhats)
                .FirstOrDefaultAsync(p => p.NhanVienId == nhanVienId && p.Thang == thang && p.Nam == nam);

            return bangLuong == null ? null : MapToDto(bangLuong);
        }

        // 4. Lấy danh sách bảng lương theo tháng (DTO)
        public async Task<List<BangLuongDto>> LayDanhSachBangLuongThangAsync(int thang, int nam)
        {
            var danhSach = await _context.BangLuongs
                .Include(p => p.NhanVien).ThenInclude(e => e.PhongBan)
                .Include(p => p.NhanVien).ThenInclude(e => e.ChucVu)
                .Where(p => p.Thang == thang && p.Nam == nam)
                .OrderBy(p => p.NhanVien.PhongBan.TenPhongBan)
                .ThenBy(p => p.NhanVien.HoTen)
                .ToListAsync();

            return danhSach.Select(MapToDto).ToList();
        }

        // 5. Duyệt bảng lương
        public async Task<bool> DuyetBangLuongAsync(int bangLuongId, string nguoiDuyet)
        {
            var bangLuong = await _context.BangLuongs.FindAsync(bangLuongId);
            if (bangLuong == null) return false;
            bangLuong.TrangThai = TrangThaiBangLuong.DaDuyet;
            bangLuong.NgayDuyet = DateTime.UtcNow;
            bangLuong.NguoiDuyet = nguoiDuyet;
            await _context.SaveChangesAsync();
            return true;
        }

        // 6. Dữ liệu Dashboard tổng hợp
        public async Task<DashboardDto> LayDuLieuDashboardAsync(int thang, int nam)
        {
            var bangLuongs = await _context.BangLuongs
                .Include(p => p.NhanVien).ThenInclude(e => e.PhongBan)
                .Where(p => p.Thang == thang && p.Nam == nam)
                .ToListAsync();

            var tongNhanVien = await _context.NhanViens.CountAsync(e => e.TrangThai == TrangThaiNhanVien.DangLamViec);

            var dashboard = new DashboardDto
            {
                Thang = thang,
                Nam = nam,
                TongNhanVien = tongNhanVien,
                TongLuongGross = bangLuongs.Sum(p => p.TongThuNhap),
                TongLuongNet = bangLuongs.Sum(p => p.ThuNhapRong),
                TongThue = bangLuongs.Sum(p => p.ThueThuNhapCaNhan),
                TongBaoHiemNhanVien = bangLuongs.Sum(p => p.BaoHiemXaHoi + p.BaoHiemYTe + p.BaoHiemThatNghiep),
                TongBaoHiemCongTy = bangLuongs.Sum(p => p.BhXhCongTy + p.BhYtCongTy + p.BhTnCongTy),
                TongChiPhiCongTy = bangLuongs.Sum(p => p.TongChiPhiCongTy),
                TongThuong = bangLuongs.Sum(p => p.TongThuong),
                TongPhat = bangLuongs.Sum(p => p.TongPhat),
                SoLuongDaDuyet = bangLuongs.Count(p => p.TrangThai == TrangThaiBangLuong.DaDuyet),
                SoLuongChoDuyet = bangLuongs.Count(p => p.TrangThai == TrangThaiBangLuong.ChoDuyet),
                SoLuongNhap = bangLuongs.Count(p => p.TrangThai == TrangThaiBangLuong.Nhap),
                TongHopTheoPhongBan = bangLuongs
                    .GroupBy(p => p.NhanVien.PhongBan.TenPhongBan)
                    .Select(g => new BaoCaoTheoPhongBanDto
                    {
                        TenPhongBan = g.Key,
                        SoLuongNhanVien = g.Count(),
                        TongLuongNet = g.Sum(p => p.ThuNhapRong),
                        TongLuongGross = g.Sum(p => p.TongThuNhap)
                    }).ToList()
            };

            return dashboard;
        }

        // Hàm chuyển Entity -> DTO
        private static BangLuongDto MapToDto(BangLuong p)
        {
            return new BangLuongDto
            {
                Id = p.Id,
                NhanVienId = p.NhanVienId,
                MaNhanVien = p.NhanVien.MaNhanVien,
                HoTen = p.NhanVien.HoTen,
                TenPhongBan = p.NhanVien.PhongBan?.TenPhongBan ?? "",
                TenChucVu = p.NhanVien.ChucVu?.TenChucVu ?? "",
                Thang = p.Thang,
                Nam = p.Nam,
                SoNgayCongThucTe = p.SoNgayCongThucTe,
                LuongCoBan = p.LuongCoBan,
                TongPhuCap = p.PhuCapAnTrua + p.PhuCapXangXe ,
                TongThuong = p.TongThuong,
                TongPhat = p.TongPhat,
                LuongLamThem = p.LuongLamThem,
                TongThuNhap = p.TongThuNhap,
                BaoHiemXaHoi = p.BaoHiemXaHoi,
                BaoHiemYTe = p.BaoHiemYTe,
                BaoHiemThatNghiep = p.BaoHiemThatNghiep,
                ThueThuNhapCaNhan = p.ThueThuNhapCaNhan,
                ThuNhapRong = p.ThuNhapRong,
                TongChiPhiCongTy = p.TongChiPhiCongTy,
                TrangThai = p.TrangThai.ToString()
            };
        }
    }
}