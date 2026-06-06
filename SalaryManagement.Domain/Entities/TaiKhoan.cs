using SalaryManagement.Domain.Common;

namespace SalaryManagement.Domain.Entities
{
    public class TaiKhoan : ThucTheCoBan
    {
        public string TenDangNhap { get; set; } = string.Empty;
        public string MatKhau { get; set; } = string.Empty;
        public string VaiTro { get; set; } = "NhanVien"; // "Admin" hoặc "NhanVien"
        public int? NhanVienId { get; set; }
        public NhanVien? NhanVien { get; set; }
    }
}