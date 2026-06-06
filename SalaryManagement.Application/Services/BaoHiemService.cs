using SalaryManagement.Application.Interfaces;

namespace SalaryManagement.Application.Services
{
    public class BaoHiemService : IBaoHiemService
    {
        private const decimal MaxLuongDongBhxh = 36_000_000m;
        private const decimal MaxLuongDongBhtn = 93_600_000m;

        public decimal TiLeBaoHiemXaHoi => 0.08m;
        public decimal TiLeBaoHiemYTe => 0.015m;
        public decimal TiLeBaoHiemThatNghiep => 0.01m;

        private decimal TinhBaoHiem(decimal luongCoBan, decimal tiLe, decimal maxLuong)
        {
            var luongTinh = Math.Min(luongCoBan, maxLuong);
            return Math.Round(luongTinh * tiLe, 0);
        }

        public decimal TinhBaoHiemXaHoiNhanVien(decimal luongCoBan)
            => TinhBaoHiem(luongCoBan, TiLeBaoHiemXaHoi, MaxLuongDongBhxh);

        public decimal TinhBaoHiemYTeNhanVien(decimal luongCoBan)
            => TinhBaoHiem(luongCoBan, TiLeBaoHiemYTe, MaxLuongDongBhxh);

        public decimal TinhBaoHiemThatNghiepNhanVien(decimal luongCoBan)
            => TinhBaoHiem(luongCoBan, TiLeBaoHiemThatNghiep, MaxLuongDongBhtn);

        public decimal TinhTongBaoHiemNhanVien(decimal luongCoBan)
            => TinhBaoHiemXaHoiNhanVien(luongCoBan)
               + TinhBaoHiemYTeNhanVien(luongCoBan)
               + TinhBaoHiemThatNghiepNhanVien(luongCoBan);

        // Công ty đóng
        public decimal TinhBaoHiemXaHoiCongTy(decimal luongCoBan)
            => TinhBaoHiem(luongCoBan, 0.175m, MaxLuongDongBhxh);

        public decimal TinhBaoHiemYTeCongTy(decimal luongCoBan)
            => TinhBaoHiem(luongCoBan, 0.03m, MaxLuongDongBhxh);

        public decimal TinhBaoHiemThatNghiepCongTy(decimal luongCoBan)
            => TinhBaoHiem(luongCoBan, 0.01m, MaxLuongDongBhtn);

        public decimal TinhTongBaoHiemCongTy(decimal luongCoBan)
            => TinhBaoHiemXaHoiCongTy(luongCoBan)
               + TinhBaoHiemYTeCongTy(luongCoBan)
               + TinhBaoHiemThatNghiepCongTy(luongCoBan);
    }
}