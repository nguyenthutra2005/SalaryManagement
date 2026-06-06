namespace SalaryManagement.Application.Interfaces
{
    public interface IBaoHiemService
    {
        decimal TiLeBaoHiemXaHoi { get; }
        decimal TiLeBaoHiemYTe { get; }
        decimal TiLeBaoHiemThatNghiep { get; }

        decimal TinhBaoHiemXaHoiNhanVien(decimal luongCoBan);
        decimal TinhBaoHiemYTeNhanVien(decimal luongCoBan);
        decimal TinhBaoHiemThatNghiepNhanVien(decimal luongCoBan);
        decimal TinhTongBaoHiemNhanVien(decimal luongCoBan);

        decimal TinhBaoHiemXaHoiCongTy(decimal luongCoBan);
        decimal TinhBaoHiemYTeCongTy(decimal luongCoBan);
        decimal TinhBaoHiemThatNghiepCongTy(decimal luongCoBan);
        decimal TinhTongBaoHiemCongTy(decimal luongCoBan);
    }
}
