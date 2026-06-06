using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalaryManagement.Infrastructure.Data;

namespace SalaryManagement.Web.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly AppDbContext _context;

        public TaiKhoanController(AppDbContext context)
        {
            _context = context;
        }

        // Trang đăng nhập
        public IActionResult DangNhap()
        {
            // Nếu đã đăng nhập thì chuyển thẳng vào dashboard
            if (HttpContext.Session.GetString("VaiTro") != null)
                return RedirectToAction("Dashboard", "BangLuong");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DangNhap(string tenDangNhap, string matKhau)
        {
            var taiKhoan = await _context.TaiKhoans
                .Include(t => t.NhanVien)
                .FirstOrDefaultAsync(t => t.TenDangNhap == tenDangNhap && t.MatKhau == matKhau && !t.DaXoa);

            if (taiKhoan == null)
            {
                ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng!";
                return View();
            }

            // Lưu thông tin vào Session
            HttpContext.Session.SetString("TenDangNhap", taiKhoan.TenDangNhap);
            HttpContext.Session.SetString("VaiTro", taiKhoan.VaiTro);
            HttpContext.Session.SetInt32("TaiKhoanId", taiKhoan.Id);
            if (taiKhoan.NhanVienId.HasValue)
                HttpContext.Session.SetInt32("NhanVienId", taiKhoan.NhanVienId.Value);

            if (taiKhoan.VaiTro == "Admin")
                return RedirectToAction("Dashboard", "BangLuong");
            else
                return RedirectToAction("PhieuLuong", "TaiKhoan");
        }

        // Trang xem phiếu lương cho nhân viên
        public async Task<IActionResult> PhieuLuong(int thang = 0, int nam = 0)
        {
            var vaiTro = HttpContext.Session.GetString("VaiTro");
            if (vaiTro == null) return RedirectToAction("DangNhap");

            if (thang == 0) thang = DateTime.Now.Month;
            if (nam == 0) nam = DateTime.Now.Year;

            var nhanVienId = HttpContext.Session.GetInt32("NhanVienId");
            if (nhanVienId == null) return RedirectToAction("DangNhap");

            var bangLuong = await _context.BangLuongs
                .Include(b => b.NhanVien).ThenInclude(n => n.PhongBan)
                .Include(b => b.NhanVien).ThenInclude(n => n.ChucVu)
                .FirstOrDefaultAsync(b => b.NhanVienId == nhanVienId && b.Thang == thang && b.Nam == nam);

            var lichSu = await _context.BangLuongs
                .Where(b => b.NhanVienId == nhanVienId)
                .OrderByDescending(b => b.Nam).ThenByDescending(b => b.Thang)
                .Take(12)
                .ToListAsync();

            ViewBag.Thang = thang;
            ViewBag.Nam = nam;
            ViewBag.LichSu = lichSu;
            return View(bangLuong);
        }

        // Đăng xuất
        public IActionResult DangXuat()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("DangNhap");
        }
    }
}