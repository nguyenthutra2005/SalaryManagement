using SalaryManagement.Domain.Common;
using SalaryManagement.Domain.Enums;

namespace SalaryManagement.Domain.Entities
{
    public class NhanVien : ThucTheCoBan
    {
        public string MaNhanVien { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? DienThoai { get; set; }
        public DateTime NgaySinh { get; set; }
        public string? GioiTinh { get; set; }
        public string? DiaChi { get; set; }
        public string? SoCMND { get; set; }
        public string? MaSoThue { get; set; }
        public string? MaBHXH { get; set; }
        public DateTime NgayVaoLam { get; set; }
        public DateTime? NgayNghiViec { get; set; }
        public TrangThaiNhanVien TrangThai { get; set; } = TrangThaiNhanVien.DangLamViec;
        public string? AnhDaiDien { get; set; }

        public int SoNguoiPhuThuoc { get; set; } = 0;
        public bool LaCuTru { get; set; } = true;

        /// <summary>
        /// Vùng lương tối thiểu (1, 2, 3, 4) theo Nghị định 293/2025/NĐ-CP
        /// Vùng I: Hà Nội, HCM | Vùng II: Tỉnh lân cận | Vùng III: Tỉnh còn lại | Vùng IV: Vùng sâu xa
        /// </summary>
        public int Vung { get; set; } = 1;

        public int PhongBanId { get; set; }
        public int ChucVuId { get; set; }

        public PhongBan PhongBan { get; set; } = null!;
        public ChucVu ChucVu { get; set; } = null!;
        public CauHinhLuong? CauHinhLuong { get; set; }
        public ICollection<BangLuong> BangLuongs { get; set; } = new List<BangLuong>();
    }
}