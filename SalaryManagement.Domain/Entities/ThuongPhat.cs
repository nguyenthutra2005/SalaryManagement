using System;
using SalaryManagement.Domain.Common;
using SalaryManagement.Domain.Enums;

namespace SalaryManagement.Domain.Entities
{
    public class ThuongPhat : ThucTheCoBan
    {
        public int NhanVienId { get; set; }
        public int? BangLuongId { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public LoaiThuongPhat Loai { get; set; }
        public string? MoTa { get; set; }
        public decimal SoTien { get; set; }
        public string? NguoiDuyet { get; set; }

        public NhanVien NhanVien { get; set; } = null!;
        public BangLuong? BangLuong { get; set; }
    }
}