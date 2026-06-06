using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalaryManagement.Domain.Entities;
using SalaryManagement.Domain.Enums;
using SalaryManagement.Infrastructure.Data;

namespace SalaryManagement.Web.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly AppDbContext _context;

        public NhanVienController(AppDbContext context)
        {
            _context = context;
        }

        private bool KiemTraAdmin() => HttpContext.Session.GetString("VaiTro") == "Admin";
        private bool KiemTraDangNhap() => HttpContext.Session.GetString("VaiTro") != null;

        public async Task<IActionResult> Index()
        {
            if (!KiemTraDangNhap()) return RedirectToAction("DangNhap", "TaiKhoan");
            if (!KiemTraAdmin()) return RedirectToAction("PhieuLuong", "TaiKhoan");

            var danhSach = await _context.NhanViens
                .Include(e => e.PhongBan)
                .Include(e => e.ChucVu)
                .Include(e => e.CauHinhLuong)
                .Where(e => !e.DaXoa)
                .OrderBy(e => e.HoTen)
                .ToListAsync();
            return View(danhSach);
        }

        public async Task<IActionResult> ThemMoi()
        {
            if (!KiemTraAdmin()) return RedirectToAction("DangNhap", "TaiKhoan");
            ViewBag.PhongBans = await _context.PhongBans.Where(e => !e.DaXoa).ToListAsync();
            ViewBag.ChucVus = await _context.ChucVus.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ThemMoi(NhanVien model, decimal luongCoBan, int soNgayCong = 26)
        {
            if (!KiemTraAdmin()) return RedirectToAction("DangNhap", "TaiKhoan");
            try
            {
                ModelState.Remove("PhongBan");
                ModelState.Remove("ChucVu");
                ModelState.Remove("CauHinhLuong");
                ModelState.Remove("BangLuongs");

                model.NgayTao = DateTime.UtcNow;
                model.TrangThai = TrangThaiNhanVien.DangLamViec;
                model.DaXoa = false;
                model.LaCuTru = true;
                model.MaNhanVien = model.MaNhanVien ?? string.Empty;
                model.HoTen = model.HoTen ?? string.Empty;

                _context.NhanViens.Add(model);
                await _context.SaveChangesAsync();

                _context.CauHinhLuongs.Add(new CauHinhLuong
                {
                    NhanVienId = model.Id,
                    LuongCoBan = luongCoBan,
                    SoNgayCongChuan = soNgayCong,
                    NgayTao = DateTime.UtcNow,
                    NgayHieuLuc = DateTime.UtcNow,
                    DaXoa = false
                });

                _context.TaiKhoans.Add(new TaiKhoan
                {
                    TenDangNhap = model.MaNhanVien,
                    MatKhau = model.MaNhanVien,
                    VaiTro = "NhanVien",
                    NhanVienId = model.Id,
                    NgayTao = DateTime.UtcNow,
                    DaXoa = false
                });

                await _context.SaveChangesAsync();
                TempData["Success"] = $"Đã thêm nhân viên {model.HoTen}! Tài khoản: {model.MaNhanVien} / {model.MaNhanVien}";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.InnerException?.Message ?? ex.Message;
                ViewBag.PhongBans = await _context.PhongBans.Where(e => !e.DaXoa).ToListAsync();
                ViewBag.ChucVus = await _context.ChucVus.ToListAsync();
                return View(model);
            }
        }

        public async Task<IActionResult> ChinhSua(int id)
        {
            if (!KiemTraAdmin()) return RedirectToAction("DangNhap", "TaiKhoan");
            var nv = await _context.NhanViens
                .Include(e => e.CauHinhLuong)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (nv == null) return NotFound();
            ViewBag.PhongBans = await _context.PhongBans.Where(e => !e.DaXoa).ToListAsync();
            ViewBag.ChucVus = await _context.ChucVus.ToListAsync();
            return View(nv);
        }

        [HttpPost]
        public async Task<IActionResult> ChinhSua(int id, NhanVien model, decimal luongCoBan, int soNgayCong = 26)
        {
            if (!KiemTraAdmin()) return RedirectToAction("DangNhap", "TaiKhoan");
            var nv = await _context.NhanViens
                .Include(e => e.CauHinhLuong)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (nv == null) return NotFound();

            nv.HoTen = model.HoTen;
            nv.Email = model.Email;
            nv.DienThoai = model.DienThoai;
            nv.PhongBanId = model.PhongBanId;
            nv.ChucVuId = model.ChucVuId;
            nv.SoNguoiPhuThuoc = model.SoNguoiPhuThuoc;
            nv.Vung = model.Vung;
            nv.NgayCapNhat = DateTime.UtcNow;

            if (nv.CauHinhLuong != null)
            {
                nv.CauHinhLuong.LuongCoBan = luongCoBan;
                nv.CauHinhLuong.SoNgayCongChuan = soNgayCong;
                nv.CauHinhLuong.NgayCapNhat = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = $"Đã cập nhật nhân viên {nv.HoTen}!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Xoa(int id)
        {
            if (!KiemTraAdmin()) return RedirectToAction("DangNhap", "TaiKhoan");
            var nv = await _context.NhanViens.FindAsync(id);
            if (nv == null) return NotFound();
            nv.DaXoa = true;
            nv.NgayCapNhat = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            TempData["Success"] = $"Đã xóa nhân viên {nv.HoTen}!";
            return RedirectToAction("Index");
        }
    }
}