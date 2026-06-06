using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using SalaryManagement.Application.Interfaces;
using SalaryManagement.Infrastructure.Data;
using System.Drawing;

namespace SalaryManagement.Application.Services
{
    public class BaoCaoService : IBaoCaoService
    {
        private readonly AppDbContext _context;
        private readonly IBangLuongService _bangLuongService;

        public BaoCaoService(AppDbContext context, IBangLuongService bangLuongService)
        {
            _context = context;
            _bangLuongService = bangLuongService;
        }

        public async Task<byte[]> XuatExcelBangLuongThangAsync(int thang, int nam)
        {
            // Lấy danh sách bảng lương DTO
            var bangLuongs = await _bangLuongService.LayDanhSachBangLuongThangAsync(thang, nam);

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add($"Luong_{thang}_{nam}");

            // Tiêu đề
            worksheet.Cell("A1").Value = $"BẢNG LƯƠNG THÁNG {thang}/{nam}";
            worksheet.Range("A1:S1").Merge().Style.Font.SetBold(true).Font.SetFontSize(14);

            // Header
            string[] headers = {
                "STT", "Mã NV", "Họ tên", "Phòng ban", "Chức vụ",
                "Ngày công", "Lương CB", "Phụ cấp", "Thưởng", "Phạt",
                "Tăng ca", "Gross", "BHXH", "BHYT", "BHTN", "Thuế TNCN",
                "Thực lĩnh", "Chi phí CT", "Trạng thái"
            };
            for (int i = 0; i < headers.Length; i++)
                worksheet.Cell(2, i + 1).Value = headers[i];

            var headerRange = worksheet.Range(2, 1, 2, headers.Length);
            headerRange.Style.Font.SetBold(true).Fill.SetBackgroundColor(XLColor.FromHtml("#1e3a5f"))
                .Font.SetFontColor(XLColor.White).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            // Dữ liệu
            int row = 3;
            for (int i = 0; i < bangLuongs.Count; i++)
            {
                var p = bangLuongs[i];
                worksheet.Cell(row, 1).Value = i + 1;
                worksheet.Cell(row, 2).Value = p.MaNhanVien;
                worksheet.Cell(row, 3).Value = p.HoTen;
                worksheet.Cell(row, 4).Value = p.TenPhongBan;
                worksheet.Cell(row, 5).Value = p.TenChucVu;
                worksheet.Cell(row, 6).Value = (double)p.SoNgayCongThucTe;
                worksheet.Cell(row, 7).Value = (double)p.LuongCoBan;
                worksheet.Cell(row, 8).Value = (double)p.TongPhuCap;
                worksheet.Cell(row, 9).Value = (double)p.TongThuong;
                worksheet.Cell(row, 10).Value = (double)p.TongPhat;
                worksheet.Cell(row, 11).Value = (double)p.LuongLamThem;
                worksheet.Cell(row, 12).Value = (double)p.TongThuNhap;
                worksheet.Cell(row, 13).Value = (double)p.BaoHiemXaHoi;
                worksheet.Cell(row, 14).Value = (double)p.BaoHiemYTe;
                worksheet.Cell(row, 15).Value = (double)p.BaoHiemThatNghiep;
                worksheet.Cell(row, 16).Value = (double)p.ThueThuNhapCaNhan;
                worksheet.Cell(row, 17).Value = (double)p.ThuNhapRong;
                worksheet.Cell(row, 18).Value = (double)p.TongChiPhiCongTy;
                worksheet.Cell(row, 19).Value = p.TrangThai;

                // Định dạng số
                for (int col = 7; col <= 18; col++)
                    worksheet.Cell(row, col).Style.NumberFormat.Format = "#,##0";
                row++;
            }

            // Tổng cộng
            worksheet.Cell(row, 3).Value = "TỔNG CỘNG";
            worksheet.Cell(row, 17).Value = bangLuongs.Sum(p => (double)p.ThuNhapRong);
            worksheet.Cell(row, 17).Style.Font.SetBold(true).NumberFormat.Format = "#,##0";

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public Task<byte[]> XuatPdfBangLuongThangAsync(int thang, int nam)
        {
            // Tạm thời chưa implement, có thể dùng QuestPDF sau
            throw new NotImplementedException("Xuất PDF sẽ được bổ sung sau.");
        }
    }
}