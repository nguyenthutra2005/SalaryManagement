using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalaryManagement.Domain.Entities;
using SalaryManagement.Domain.Enums;
using SalaryManagement.Infrastructure.Data;

namespace SalaryManagement.Web.Controllers
{
    public class ThuongPhatController : Controller
    {
        private readonly AppDbContext _context;

        public ThuongPhatController(AppDbContext context)
        {
            _context = context;
        }
        private bool KiemTraAdmin()
        {
            return HttpContext.Session.GetString("VaiTro") == "Admin";
        }

        private bool KiemTraDangNhap()
        {
            return HttpContext.Session.GetString("VaiTro") != null;
        }
        public async Task<IActionResult> Index()
        {
            if (!KiemTraAdmin()) return RedirectToAction("DangNhap", "TaiKhoan");

            var danhSach = await _context.ThuongPhats
                .Include(t => t.NhanVien)
                .OrderByDescending(t => t.Nam)
                .ThenByDescending(t => t.Thang)
                .ToListAsync();
            return View(danhSach);
        }

        public async Task<IActionResult> ThemMoi()
        {
            if (!KiemTraAdmin()) return RedirectToAction("DangNhap", "TaiKhoan");

            ViewBag.NhanViens = await _context.NhanViens
                .Where(e => !e.DaXoa && e.TrangThai == TrangThaiNhanVien.DangLamViec)
                .OrderBy(e => e.HoTen)
                .ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ThemMoi(ThuongPhat model)
        {
            if (!KiemTraAdmin()) return RedirectToAction("DangNhap", "TaiKhoan");

            model.NgayTao = DateTime.UtcNow;
            model.MoTa = model.MoTa ?? string.Empty;
            model.DaXoa = false;
            _context.ThuongPhats.Add(model);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Đã thêm thưởng/phạt thành công!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Xoa(int id)
        {
            if (!KiemTraAdmin()) return RedirectToAction("DangNhap", "TaiKhoan");

            var tp = await _context.ThuongPhats.FindAsync(id);
            if (tp != null)
            {
                _context.ThuongPhats.Remove(tp);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Đã xóa thành công!";
            }
            return RedirectToAction("Index");
        }
    }
}
