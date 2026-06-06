using SalaryManagement.Domain.Common;

namespace SalaryManagement.Domain.Entities
{
    public class ChamCong : ThucTheCoBan
    {
        public int NhanVienId { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public int SoNgayLamViec { get; set; }
        public int SoNgayNghi { get; set; } = 0;
        public int SoNgayNghiPhep { get; set; } = 0;
        public decimal SoGioTangCa { get; set; } = 0;
        public string? GhiChu { get; set; }

        public NhanVien NhanVien { get; set; } = null!;
    }
}