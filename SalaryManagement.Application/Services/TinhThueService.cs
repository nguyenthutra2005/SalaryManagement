using SalaryManagement.Application.Interfaces;

namespace SalaryManagement.Application.Services
{
    public class TinhThueService : ITinhThueService
    {
        private const decimal GiamTruBanThan = 15_500_000m;
        private const decimal GiamTruNguoiPhuThuoc = 6_200_000m;

        private static readonly (decimal Max, decimal Rate, decimal QuickDeduction)[] BacThue =
        {
            (10_000_000m, 0.05m, 0m),
            (30_000_000m, 0.10m, 500_000m),
            (60_000_000m, 0.20m, 3_500_000m),
            (100_000_000m, 0.30m, 9_500_000m),
            (decimal.MaxValue, 0.35m, 14_500_000m),
        };

        public decimal LayGiamTruBanThan() => GiamTruBanThan;
        public decimal LayGiamTruNguoiPhuThuoc() => GiamTruNguoiPhuThuoc;

        public decimal TinhThuNhapChiuThue(decimal tongThuNhap, decimal tongBaoHiem, int soNguoiPhuThuoc, bool laCuTru = true)
        {
            if (!laCuTru) return tongThuNhap;
            var giamTru = GiamTruBanThan + (soNguoiPhuThuoc * GiamTruNguoiPhuThuoc) + tongBaoHiem;
            return Math.Max(0, tongThuNhap - giamTru);
        }

        public decimal TinhThueThuNhapCaNhan(decimal thuNhapChiuThue)
        {
            if (thuNhapChiuThue <= 0) return 0;
            foreach (var (max, rate, quickDeduction) in BacThue)
            {
                if (thuNhapChiuThue <= max || max == decimal.MaxValue)
                    return Math.Round(thuNhapChiuThue * rate - quickDeduction, 0);
            }
            return 0;
        }
    }
}