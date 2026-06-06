using SalaryManagement.Domain.Entities;
using SalaryManagement.Application.DTOs;

namespace SalaryManagement.Application.Interfaces
{
    public interface IBangLuongService
    {
        Task<BangLuong> TaoBangLuongAsync(int nhanVienId, int thang, int nam, decimal soNgayCongThucTe, decimal soGioLamThem = 0);
        Task<List<BangLuong>> TaoBangLuongHangThangAsync(int thang, int nam);
        Task<BangLuongDto?> LayBangLuongAsync(int nhanVienId, int thang, int nam);
        Task<List<BangLuongDto>> LayDanhSachBangLuongThangAsync(int thang, int nam);
        Task<bool> DuyetBangLuongAsync(int bangLuongId, string nguoiDuyet);
        Task<DashboardDto> LayDuLieuDashboardAsync(int thang, int nam);
    }
}