using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalaryManagement.Domain.Entities;
using SalaryManagement.Domain.Enums;
using SalaryManagement.Infrastructure.Data;

namespace SalaryManagement.Web.Controllers
{
    public class ChamCongController : Controller
    {
        private readonly AppDbContext _context;

        public ChamCongController(AppDbContext context)
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
        public async Task<IActionResult> Index(int thang = 0, int nam = 0)
        {
            if (!KiemTraDangNhap()) return RedirectToAction("DangNhap", "TaiKhoan");
            if (!KiemTraAdmin()) return RedirectToAction("PhieuLuong", "TaiKhoan");
            if (thang == 0) thang = DateTime.Now.Month;
            if (nam == 0) nam = DateTime.Now.Year;

            var danhSach = await _context.ChamCongs
                .Include(c => c.NhanVien).ThenInclude(e => e.PhongBan)
                .Where(c => c.Thang == thang && c.Nam == nam)
                .OrderBy(c => c.NhanVien.HoTen)
                .ToListAsync();

            ViewBag.Thang = thang;
            ViewBag.Nam = nam;
            return View(danhSach);
        }

        public async Task<IActionResult> NhapChamCong(int thang = 0, int nam = 0)
        {
            if (!KiemTraDangNhap()) return RedirectToAction("DangNhap", "TaiKhoan");
            if (!KiemTraAdmin()) return RedirectToAction("PhieuLuong", "TaiKhoan");
            if (thang == 0) thang = DateTime.Now.Month;
            if (nam == 0) nam = DateTime.Now.Year;

            var nhanViens = await _context.NhanViens
                .Include(e => e.PhongBan)
                .Where(e => !e.DaXoa && e.TrangThai == TrangThaiNhanVien.DangLamViec)
                .OrderBy(e => e.HoTen)
                .ToListAsync();

            var chamCongs = await _context.ChamCongs
                .Where(c => c.Thang == thang && c.Nam == nam)
                .ToListAsync();

            ViewBag.Thang = thang;
            ViewBag.Nam = nam;
            ViewBag.NhanViens = nhanViens;
            ViewBag.ChamCongs = chamCongs;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LuuChamCong(int thang, int nam, List<int> nhanVienIds,
            List<int> soNgayLamViec, List<int> soNgayNghi, List<decimal> soGioTangCa)
        {
            if (!KiemTraDangNhap()) return RedirectToAction("DangNhap", "TaiKhoan");
            if (!KiemTraAdmin()) return RedirectToAction("PhieuLuong", "TaiKhoan");
            for (int i = 0; i < nhanVienIds.Count; i++)
            {
                var existing = await _context.ChamCongs
                    .FirstOrDefaultAsync(c => c.NhanVienId == nhanVienIds[i] && c.Thang == thang && c.Nam == nam);

                if (existing != null)
                {
                    existing.SoNgayLamViec = soNgayLamViec[i];
                    existing.SoNgayNghi = soNgayNghi[i];
                    existing.SoGioTangCa = soGioTangCa[i];
                    existing.NgayCapNhat = DateTime.UtcNow;
                }
                else
                {
                    _context.ChamCongs.Add(new ChamCong
                    {
                        NhanVienId = nhanVienIds[i],
                        Thang = thang,
                        Nam = nam,
                        SoNgayLamViec = soNgayLamViec[i],
                        SoNgayNghi = soNgayNghi[i],
                        SoGioTangCa = soGioTangCa[i],
                        NgayTao = DateTime.UtcNow
                    });
                }
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = $"Đã lưu chấm công tháng {thang}/{nam}!";
            return RedirectToAction("Index", new { thang, nam });
        }
    }
}