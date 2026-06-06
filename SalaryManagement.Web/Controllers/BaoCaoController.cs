using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Interfaces;
using SalaryManagement.Infrastructure.Data;

namespace SalaryManagement.Web.Controllers
{
    public class BaoCaoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBangLuongService _bangLuongService;

        public BaoCaoController(AppDbContext context, IBangLuongService bangLuongService)
        {
            _context = context;
            _bangLuongService = bangLuongService;
        }
        private bool KiemTraAdmin()
        {
            return HttpContext.Session.GetString("VaiTro") == "Admin";
        }

        private bool KiemTraDangNhap()
        {
            return HttpContext.Session.GetString("VaiTro") != null;
        }
        public async Task<IActionResult> Index(int thang = 0, int nam = 0)
        {
            if (!KiemTraAdmin()) return RedirectToAction("DangNhap", "TaiKhoan");
            if (thang == 0) thang = DateTime.Now.Month;
            if (nam == 0) nam = DateTime.Now.Year;

            var dashboard = await _bangLuongService.LayDuLieuDashboardAsync(thang, nam);
            ViewBag.Thang = thang;
            ViewBag.Nam = nam;
            return View(dashboard);
        }

        public async Task<IActionResult> XuatFile(int thang = 0, int nam = 0)
        {
            if (!KiemTraAdmin()) return RedirectToAction("DangNhap", "TaiKhoan");
            if (thang == 0) thang = DateTime.Now.Month;
            if (nam == 0) nam = DateTime.Now.Year;

            var danhSach = await _bangLuongService.LayDanhSachBangLuongThangAsync(thang, nam);
            ViewBag.Thang = thang;
            ViewBag.Nam = nam;
            return View(danhSach);
        }

        public async Task<IActionResult> XuatExcel(int thang, int nam)
        {
            if (!KiemTraAdmin()) return RedirectToAction("DangNhap", "TaiKhoan");
            var danhSach = await _bangLuongService.LayDanhSachBangLuongThangAsync(thang, nam);

            var csv = "Mã NV,Họ Tên,Phòng Ban,Chức Vụ,Ngày Công,Lương CB,Phụ Cấp,Thưởng,Phạt,Gross,BHXH,BHYT,BHTN,Thuế TNCN,Net\n";
            foreach (var bl in danhSach)
            {
                csv += $"{bl.MaNhanVien},{bl.HoTen},{bl.TenPhongBan},{bl.TenChucVu}," +
                       $"{bl.SoNgayCongThucTe},{bl.LuongCoBan},{bl.TongPhuCap}," +
                       $"{bl.TongThuong},{bl.TongPhat},{bl.TongThuNhap}," +
                       $"{bl.BaoHiemXaHoi},{bl.BaoHiemYTe},{bl.BaoHiemThatNghiep}," +
                       $"{bl.ThueThuNhapCaNhan},{bl.ThuNhapRong}\n";
            }

            // Thêm BOM UTF-8 để Excel hiển thị đúng tiếng Việt
            var bom = new byte[] { 0xEF, 0xBB, 0xBF };
            var bytes = bom.Concat(System.Text.Encoding.UTF8.GetBytes(csv)).ToArray();
            return File(bytes, "text/csv", $"BangLuong_T{thang}_{nam}.csv");
        }
    }
}