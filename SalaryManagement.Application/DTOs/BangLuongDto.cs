namespace SalaryManagement.Application.DTOs
{
    public class BangLuongDto
    {
        public int Id { get; set; }
        public int NhanVienId { get; set; }
        public string MaNhanVien { get; set; } = "";
        public string HoTen { get; set; } = "";
        public string TenPhongBan { get; set; } = "";
        public string TenChucVu { get; set; } = "";
        public int Thang { get; set; }
        public int Nam { get; set; }
        public decimal SoNgayCongThucTe { get; set; }
        public decimal LuongCoBan { get; set; }
        public decimal TongPhuCap { get; set; }
        public decimal TongThuong { get; set; }
        public decimal TongPhat { get; set; }
        public decimal LuongLamThem { get; set; }
        public decimal TongThuNhap { get; set; }
        public decimal BaoHiemXaHoi { get; set; }
        public decimal BaoHiemYTe { get; set; }
        public decimal BaoHiemThatNghiep { get; set; }
        public decimal ThueThuNhapCaNhan { get; set; }
        public decimal ThuNhapRong { get; set; }
        public decimal TongChiPhiCongTy { get; set; }
        public string TrangThai { get; set; } = "";
    }

    public class DashboardDto
    {
        public int Thang { get; set; }
        public int Nam { get; set; }
        public int TongNhanVien { get; set; }
        public decimal TongLuongGross { get; set; }
        public decimal TongLuongNet { get; set; }
        public decimal TongThue { get; set; }
        public decimal TongBaoHiemNhanVien { get; set; }
        public decimal TongBaoHiemCongTy { get; set; }
        public decimal TongChiPhiCongTy { get; set; }
        public decimal TongThuong { get; set; }
        public decimal TongPhat { get; set; }
        public int SoLuongDaDuyet { get; set; }
        public int SoLuongChoDuyet { get; set; }
        public int SoLuongNhap { get; set; }
        public List<BaoCaoTheoPhongBanDto> TongHopTheoPhongBan { get; set; } = new();
    }

    public class BaoCaoTheoPhongBanDto
    {
        public string TenPhongBan { get; set; } = "";
        public int SoLuongNhanVien { get; set; }
        public decimal TongLuongNet { get; set; }
        public decimal TongLuongGross { get; set; }
    }
}

