using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CauHinhHeThong2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PhuCapDienThoai",
                table: "ChucVus",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PhuCapNhaO",
                table: "ChucVus",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PhuCapTrachNhiem",
                table: "ChucVus",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "CauHinhHeThongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhuCapAnTruaMoiNgay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhuCapXangXeMoiNgay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayHieuLuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DaXoa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHinhHeThongs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CauHinhHeThongs",
                columns: new[] { "Id", "DaXoa", "GhiChu", "NgayCapNhat", "NgayHieuLuc", "NgayTao", "PhuCapAnTruaMoiNgay", "PhuCapXangXeMoiNgay" },
                values: new object[] { 1, false, "Cấu hình mặc định", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 30000m, 20000m });

            migrationBuilder.UpdateData(
                table: "ChucVus",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PhuCapDienThoai", "PhuCapNhaO", "PhuCapTrachNhiem" },
                values: new object[] { 500000m, 1000000m, 2000000m });

            migrationBuilder.UpdateData(
                table: "ChucVus",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PhuCapDienThoai", "PhuCapNhaO", "PhuCapTrachNhiem" },
                values: new object[] { 300000m, 500000m, 1000000m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CauHinhHeThongs");

            migrationBuilder.DropColumn(
                name: "PhuCapDienThoai",
                table: "ChucVus");

            migrationBuilder.DropColumn(
                name: "PhuCapNhaO",
                table: "ChucVus");

            migrationBuilder.DropColumn(
                name: "PhuCapTrachNhiem",
                table: "ChucVus");
        }
    }
}
