using SalaryManagement.Domain.Common;

namespace SalaryManagement.Domain.Entities
{
    public class PhongBan : ThucTheCoBan
    {
        public string MaPhongBan { get; set; } = string.Empty;
        public string TenPhongBan { get; set; } = string.Empty;
        public string? MoTa { get; set; }
        public int? TruongPhongId { get; set; }

        public ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
    }
}