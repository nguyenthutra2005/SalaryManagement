using System;
using SalaryManagement.Domain.Common;

namespace SalaryManagement.Domain.Entities
{
    public class CauHinhLuong : ThucTheCoBan
    {
        public int NhanVienId { get; set; }
        public decimal LuongCoBan { get; set; }
        public int SoNgayCongChuan { get; set; } = 26;

        public decimal PhuCapAnTrua { get; set; }
        public decimal PhuCapXangXe { get; set; }
        public decimal PhuCapTrachNhiem { get; set; }
        public decimal PhuCapDienThoai { get; set; }
        public decimal PhuCapNhaO { get; set; }

        public DateTime NgayHieuLuc { get; set; }
        public DateTime? NgayKetThuc { get; set; }

        public NhanVien NhanVien { get; set; } = null!;
    }
}