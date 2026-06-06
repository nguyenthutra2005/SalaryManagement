using SalaryManagement.Domain.Common;

namespace SalaryManagement.Domain.Entities
{
    public class ChucVu : ThucTheCoBan
    {
        public string MaChucVu { get; set; } = string.Empty;
        public string TenChucVu { get; set; } = string.Empty;
        public int CapBac { get; set; }
        public decimal LuongToiThieu { get; set; }
        public decimal LuongToiDa { get; set; }

        // Phụ cấp cố định theo chức vụ
        public decimal PhuCapTrachNhiem { get; set; } = 0;
        public decimal PhuCapDienThoai { get; set; } = 0;
        public decimal PhuCapNhaO { get; set; } = 0;

        public ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
    }
}