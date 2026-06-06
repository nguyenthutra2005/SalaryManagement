using System;
using System.Collections.Generic;
using SalaryManagement.Domain.Common;
using SalaryManagement.Domain.Enums;

namespace SalaryManagement.Domain.Entities
{
    public class BangLuong : ThucTheCoBan
    {
        public int NhanVienId { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }

        public int SoNgayCongChuan { get; set; }
        public decimal SoNgayCongThucTe { get; set; }
        public decimal SoGioLamThem { get; set; }

        public decimal LuongCoBan { get; set; }

        public decimal PhuCapAnTrua { get; set; }
        public decimal PhuCapXangXe { get; set; }
        public decimal PhuCapTrachNhiem { get; set; }
        public decimal PhuCapDienThoai { get; set; }
        public decimal PhuCapNhaO { get; set; }

        public decimal LuongTheoNgayCong { get; set; }
        public decimal TongThuong { get; set; }
        public decimal TongPhat { get; set; }
        public decimal LuongLamThem { get; set; }

        public decimal TongThuNhap { get; set; }  // Gross

        public decimal BaoHiemXaHoi { get; set; }
        public decimal BaoHiemYTe { get; set; }
        public decimal BaoHiemThatNghiep { get; set; }

        public decimal ThuNhapChiuThue { get; set; }
        public decimal GiamTruBanThan { get; set; }
        public decimal GiamTruNguoiPhuThuoc { get; set; }

        public decimal ThueThuNhapCaNhan { get; set; }

        public decimal ThuNhapRong { get; set; }

        public decimal BhXhCongTy { get; set; }
        public decimal BhYtCongTy { get; set; }
        public decimal BhTnCongTy { get; set; }
        public decimal TongChiPhiCongTy { get; set; }

        public string? GhiChu { get; set; }
        public TrangThaiBangLuong TrangThai { get; set; } = TrangThaiBangLuong.Nhap;
        public DateTime? NgayDuyet { get; set; }
        public string? NguoiDuyet { get; set; }

        public NhanVien NhanVien { get; set; } = null!;
        public ICollection<ThuongPhat> ThuongPhats { get; set; } = new List<ThuongPhat>();
    }
}