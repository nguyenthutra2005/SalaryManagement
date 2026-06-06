using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SalaryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChucVus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaChucVu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenChucVu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapBac = table.Column<int>(type: "int", nullable: false),
                    LuongToiThieu = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LuongToiDa = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DaXoa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChucVus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhongBans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhongBan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenPhongBan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TruongPhongId = table.Column<int>(type: "int", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DaXoa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongBans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NhanViens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNhanVien = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoCMND = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaSoThue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaBHXH = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayVaoLam = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayNghiViec = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoNguoiPhuThuoc = table.Column<int>(type: "int", nullable: false),
                    LaCuTru = table.Column<bool>(type: "bit", nullable: false),
                    PhongBanId = table.Column<int>(type: "int", nullable: false),
                    ChucVuId = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DaXoa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanViens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NhanViens_ChucVus_ChucVuId",
                        column: x => x.ChucVuId,
                        principalTable: "ChucVus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NhanViens_PhongBans_PhongBanId",
                        column: x => x.PhongBanId,
                        principalTable: "PhongBans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BangLuongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NhanVienId = table.Column<int>(type: "int", nullable: false),
                    Thang = table.Column<int>(type: "int", nullable: false),
                    Nam = table.Column<int>(type: "int", nullable: false),
                    SoNgayCongChuan = table.Column<int>(type: "int", nullable: false),
                    SoNgayCongThucTe = table.Column<decimal>(type: "decimal(5,1)", nullable: false),
                    SoGioLamThem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LuongCoBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhuCapAnTrua = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhuCapXangXe = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhuCapTrachNhiem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhuCapDienThoai = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhuCapNhaO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LuongTheoNgayCong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongThuong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongPhat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LuongLamThem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongThuNhap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BaoHiemXaHoi = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BaoHiemYTe = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BaoHiemThatNghiep = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThuNhapChiuThue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GiamTruBanThan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GiamTruNguoiPhuThuoc = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThueThuNhapCaNhan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThuNhapRong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BhXhCongTy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BhYtCongTy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BhTnCongTy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongChiPhiCongTy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    NgayDuyet = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiDuyet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DaXoa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BangLuongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BangLuongs_NhanViens_NhanVienId",
                        column: x => x.NhanVienId,
                        principalTable: "NhanViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CauHinhLuongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NhanVienId = table.Column<int>(type: "int", nullable: false),
                    LuongCoBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoNgayCongChuan = table.Column<int>(type: "int", nullable: false),
                    PhuCapAnTrua = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhuCapXangXe = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhuCapTrachNhiem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhuCapDienThoai = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhuCapNhaO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayHieuLuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DaXoa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHinhLuongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CauHinhLuongs_NhanViens_NhanVienId",
                        column: x => x.NhanVienId,
                        principalTable: "NhanViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThuongPhats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NhanVienId = table.Column<int>(type: "int", nullable: false),
                    BangLuongId = table.Column<int>(type: "int", nullable: true),
                    Thang = table.Column<int>(type: "int", nullable: false),
                    Nam = table.Column<int>(type: "int", nullable: false),
                    Loai = table.Column<int>(type: "int", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiDuyet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DaXoa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuongPhats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThuongPhats_BangLuongs_BangLuongId",
                        column: x => x.BangLuongId,
                        principalTable: "BangLuongs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ThuongPhats_NhanViens_NhanVienId",
                        column: x => x.NhanVienId,
                        principalTable: "NhanViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ChucVus",
                columns: new[] { "Id", "CapBac", "DaXoa", "LuongToiDa", "LuongToiThieu", "MaChucVu", "NgayCapNhat", "NgayTao", "TenChucVu" },
                values: new object[,]
                {
                    { 1, 10, false, 200000000m, 50000000m, "CEO", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Giám đốc" },
                    { 2, 7, false, 50000000m, 20000000m, "MGR", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Trưởng phòng" }
                });

            migrationBuilder.InsertData(
                table: "PhongBans",
                columns: new[] { "Id", "DaXoa", "MaPhongBan", "MoTa", "NgayCapNhat", "NgayTao", "TenPhongBan", "TruongPhongId" },
                values: new object[,]
                {
                    { 1, false, "IT", null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Phòng Công nghệ Thông tin", null },
                    { 2, false, "HR", null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Phòng Nhân sự", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BangLuongs_NhanVienId_Thang_Nam",
                table: "BangLuongs",
                columns: new[] { "NhanVienId", "Thang", "Nam" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CauHinhLuongs_NhanVienId",
                table: "CauHinhLuongs",
                column: "NhanVienId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhanViens_ChucVuId",
                table: "NhanViens",
                column: "ChucVuId");

            migrationBuilder.CreateIndex(
                name: "IX_NhanViens_Email",
                table: "NhanViens",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NhanViens_MaNhanVien",
                table: "NhanViens",
                column: "MaNhanVien",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhanViens_PhongBanId",
                table: "NhanViens",
                column: "PhongBanId");

            migrationBuilder.CreateIndex(
                name: "IX_ThuongPhats_BangLuongId",
                table: "ThuongPhats",
                column: "BangLuongId");

            migrationBuilder.CreateIndex(
                name: "IX_ThuongPhats_NhanVienId",
                table: "ThuongPhats",
                column: "NhanVienId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CauHinhLuongs");

            migrationBuilder.DropTable(
                name: "ThuongPhats");

            migrationBuilder.DropTable(
                name: "BangLuongs");

            migrationBuilder.DropTable(
                name: "NhanViens");

            migrationBuilder.DropTable(
                name: "ChucVus");

            migrationBuilder.DropTable(
                name: "PhongBans");
        }
    }
}
