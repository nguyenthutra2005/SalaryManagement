using SalaryManagement.Application.DTOs;

namespace SalaryManagement.Application.Interfaces
{
    public interface IBaoCaoService
    {
        // Xuất bảng lương tháng ra file Excel (byte array để trả về trình duyệt)
        Task<byte[]> XuatExcelBangLuongThangAsync(int thang, int nam);

        // Xuất bảng lương tháng ra file PDF (tạm thời có thể để trống hoặc throw)
        Task<byte[]> XuatPdfBangLuongThangAsync(int thang, int nam);
    }
}