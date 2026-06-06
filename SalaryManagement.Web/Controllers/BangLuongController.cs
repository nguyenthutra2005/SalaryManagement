using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Application.Interfaces;

namespace SalaryManagement.Web.Controllers
{
    public class BangLuongController : Controller
    {
        private readonly IBangLuongService _bangLuongService;

        public BangLuongController(IBangLuongService bangLuongService)
        {
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

        // Dashboard
        public async Task<IActionResult> Dashboard(int thang = 0, int nam = 0)
        {
            if (!KiemTraDangNhap()) return RedirectToAction("DangNhap", "TaiKhoan");
            if (!KiemTraAdmin()) return RedirectToAction("PhieuLuong", "TaiKhoan");

            if (thang == 0) thang = DateTime.Now.Month;
            if (nam == 0) nam = DateTime.Now.Year;

            var dashboard = await _bangLuongService.LayDuLieuDashboardAsync(thang, nam);
            ViewBag.Thang = thang;
            ViewBag.Nam = nam;
            return View(dashboard);
        }

        // Danh sách bảng lương
        public async Task<IActionResult> Index(int thang = 0, int nam = 0)
        {
            if (!KiemTraDangNhap()) return RedirectToAction("DangNhap", "TaiKhoan");
            if (!KiemTraAdmin()) return RedirectToAction("PhieuLuong", "TaiKhoan");

            if (thang == 0) thang = DateTime.Now.Month;
            if (nam == 0) nam = DateTime.Now.Year;

            var danhSach = await _bangLuongService.LayDanhSachBangLuongThangAsync(thang, nam);
            ViewBag.Thang = thang;
            ViewBag.Nam = nam;
            return View(danhSach);
        }

        // Chi tiết bảng lương
        public async Task<IActionResult> ChiTiet(int nhanVienId, int thang, int nam)
        {
            if (!KiemTraDangNhap()) return RedirectToAction("DangNhap", "TaiKhoan");
            if (!KiemTraAdmin()) return RedirectToAction("PhieuLuong", "TaiKhoan");

            var bangLuong = await _bangLuongService.LayBangLuongAsync(nhanVienId, thang, nam);
            if (bangLuong == null) return NotFound();
            return View(bangLuong);
        }

        // Tạo bảng lương hàng loạt
        [HttpPost]
        public async Task<IActionResult> TaoHangLoat(int thang, int nam)
        {
            if (!KiemTraAdmin()) return RedirectToAction("DangNhap", "TaiKhoan");

            await _bangLuongService.TaoBangLuongHangThangAsync(thang, nam);
            TempData["Success"] = $"Đã tạo bảng lương tháng {thang}/{nam} thành công!";
            return RedirectToAction("Index", new { thang, nam });
        }

        // Duyệt bảng lương
        [HttpPost]
        public async Task<IActionResult> Duyet(int id, int thang, int nam)
        {
            if (!KiemTraAdmin()) return RedirectToAction("DangNhap", "TaiKhoan");

            await _bangLuongService.DuyetBangLuongAsync(id, "Admin");
            TempData["Success"] = "Đã duyệt bảng lương thành công!";
            return RedirectToAction("Index", new { thang, nam });
        }

        // Tính thuế TNCN
        public IActionResult TinhThue()
        {
            if (!KiemTraDangNhap()) return RedirectToAction("DangNhap", "TaiKhoan");
            if (!KiemTraAdmin()) return RedirectToAction("PhieuLuong", "TaiKhoan");
            return View();
        }
    }
}