using SalaryManagement.Domain.Common;

namespace SalaryManagement.Domain.Entities
{
    public class CauHinhHeThong : ThucTheCoBan
    {
        /// <summary>
        /// Phụ cấp ăn trưa mỗi ngày công (áp dụng toàn công ty)
        /// </summary>
        public decimal PhuCapAnTruaMoiNgay { get; set; } = 30_000m;

        /// <summary>
        /// Phụ cấp xăng xe mỗi ngày công (áp dụng toàn công ty)
        /// </summary>
        public decimal PhuCapXangXeMoiNgay { get; set; } = 20_000m;

        /// <summary>
        /// Ngày hiệu lực của cấu hình
        /// </summary>
        public DateTime NgayHieuLuc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? GhiChu { get; set; }
    }
}