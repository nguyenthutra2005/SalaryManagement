namespace SalaryManagement.Application.Interfaces
{
    public interface ITinhThueService
    {
        decimal TinhThueThuNhapCaNhan(decimal thuNhapChiuThue);
        decimal TinhThuNhapChiuThue(decimal tongThuNhap, decimal tongBaoHiem, int soNguoiPhuThuoc, bool laCuTru = true);
        decimal LayGiamTruBanThan();
        decimal LayGiamTruNguoiPhuThuoc();
    }
}
