using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SalaryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CauHinhHeThongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhuCapAnTruaMoiNgay = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PhuCapXangXeMoiNgay = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    NgayHieuLuc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GhiChu = table.Column<string>(type: "text", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DaXoa = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHinhHeThongs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChucVus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaChucVu = table.Column<string>(type: "text", nullable: false),
                    TenChucVu = table.Column<string>(type: "text", nullable: false),
                    CapBac = table.Column<int>(type: "integer", nullable: false),
                    LuongToiThieu = table.Column<decimal>(type: "numeric", nullable: false),
                    LuongToiDa = table.Column<decimal>(type: "numeric", nullable: false),
                    PhuCapTrachNhiem = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PhuCapDienThoai = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PhuCapNhaO = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DaXoa = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChucVus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhongBans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaPhongBan = table.Column<string>(type: "text", nullable: false),
                    TenPhongBan = table.Column<string>(type: "text", nullable: false),
                    MoTa = table.Column<string>(type: "text", nullable: true),
                    TruongPhongId = table.Column<int>(type: "integer", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DaXoa = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongBans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NhanViens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaNhanVien = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    HoTen = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    DienThoai = table.Column<string>(type: "text", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GioiTinh = table.Column<string>(type: "text", nullable: true),
                    DiaChi = table.Column<string>(type: "text", nullable: true),
                    SoCMND = table.Column<string>(type: "text", nullable: true),
                    MaSoThue = table.Column<string>(type: "text", nullable: true),
                    MaBHXH = table.Column<string>(type: "text", nullable: true),
                    NgayVaoLam = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NgayNghiViec = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TrangThai = table.Column<int>(type: "integer", nullable: false),
                    AnhDaiDien = table.Column<string>(type: "text", nullable: true),
                    SoNguoiPhuThuoc = table.Column<int>(type: "integer", nullable: false),
                    LaCuTru = table.Column<bool>(type: "boolean", nullable: false),
                    Vung = table.Column<int>(type: "integer", nullable: false),
                    PhongBanId = table.Column<int>(type: "integer", nullable: false),
                    ChucVuId = table.Column<int>(type: "integer", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DaXoa = table.Column<bool>(type: "boolean", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NhanVienId = table.Column<int>(type: "integer", nullable: false),
                    Thang = table.Column<int>(type: "integer", nullable: false),
                    Nam = table.Column<int>(type: "integer", nullable: false),
                    SoNgayCongChuan = table.Column<int>(type: "integer", nullable: false),
                    SoNgayCongThucTe = table.Column<decimal>(type: "numeric(5,1)", nullable: false),
                    SoGioLamThem = table.Column<decimal>(type: "numeric", nullable: false),
                    LuongCoBan = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PhuCapAnTrua = table.Column<decimal>(type: "numeric", nullable: false),
                    PhuCapXangXe = table.Column<decimal>(type: "numeric", nullable: false),
                    PhuCapTrachNhiem = table.Column<decimal>(type: "numeric", nullable: false),
                    PhuCapDienThoai = table.Column<decimal>(type: "numeric", nullable: false),
                    PhuCapNhaO = table.Column<decimal>(type: "numeric", nullable: false),
                    LuongTheoNgayCong = table.Column<decimal>(type: "numeric", nullable: false),
                    TongThuong = table.Column<decimal>(type: "numeric", nullable: false),
                    TongPhat = table.Column<decimal>(type: "numeric", nullable: false),
                    LuongLamThem = table.Column<decimal>(type: "numeric", nullable: false),
                    TongThuNhap = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    BaoHiemXaHoi = table.Column<decimal>(type: "numeric", nullable: false),
                    BaoHiemYTe = table.Column<decimal>(type: "numeric", nullable: false),
                    BaoHiemThatNghiep = table.Column<decimal>(type: "numeric", nullable: false),
                    ThuNhapChiuThue = table.Column<decimal>(type: "numeric", nullable: false),
                    GiamTruBanThan = table.Column<decimal>(type: "numeric", nullable: false),
                    GiamTruNguoiPhuThuoc = table.Column<decimal>(type: "numeric", nullable: false),
                    ThueThuNhapCaNhan = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ThuNhapRong = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    BhXhCongTy = table.Column<decimal>(type: "numeric", nullable: false),
                    BhYtCongTy = table.Column<decimal>(type: "numeric", nullable: false),
                    BhTnCongTy = table.Column<decimal>(type: "numeric", nullable: false),
                    TongChiPhiCongTy = table.Column<decimal>(type: "numeric", nullable: false),
                    GhiChu = table.Column<string>(type: "text", nullable: true),
                    TrangThai = table.Column<int>(type: "integer", nullable: false),
                    NgayDuyet = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    NguoiDuyet = table.Column<string>(type: "text", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DaXoa = table.Column<bool>(type: "boolean", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NhanVienId = table.Column<int>(type: "integer", nullable: false),
                    LuongCoBan = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    SoNgayCongChuan = table.Column<int>(type: "integer", nullable: false),
                    PhuCapAnTrua = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PhuCapXangXe = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PhuCapTrachNhiem = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PhuCapDienThoai = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PhuCapNhaO = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    NgayHieuLuc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DaXoa = table.Column<bool>(type: "boolean", nullable: false)
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
                name: "ChamCongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NhanVienId = table.Column<int>(type: "integer", nullable: false),
                    Thang = table.Column<int>(type: "integer", nullable: false),
                    Nam = table.Column<int>(type: "integer", nullable: false),
                    SoNgayLamViec = table.Column<int>(type: "integer", nullable: false),
                    SoNgayNghi = table.Column<int>(type: "integer", nullable: false),
                    SoNgayNghiPhep = table.Column<int>(type: "integer", nullable: false),
                    SoGioTangCa = table.Column<decimal>(type: "numeric", nullable: false),
                    GhiChu = table.Column<string>(type: "text", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DaXoa = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChamCongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChamCongs_NhanViens_NhanVienId",
                        column: x => x.NhanVienId,
                        principalTable: "NhanViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenDangNhap = table.Column<string>(type: "text", nullable: false),
                    MatKhau = table.Column<string>(type: "text", nullable: false),
                    VaiTro = table.Column<string>(type: "text", nullable: false),
                    NhanVienId = table.Column<int>(type: "integer", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DaXoa = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaiKhoans_NhanViens_NhanVienId",
                        column: x => x.NhanVienId,
                        principalTable: "NhanViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ThuongPhats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NhanVienId = table.Column<int>(type: "integer", nullable: false),
                    BangLuongId = table.Column<int>(type: "integer", nullable: true),
                    Thang = table.Column<int>(type: "integer", nullable: false),
                    Nam = table.Column<int>(type: "integer", nullable: false),
                    Loai = table.Column<int>(type: "integer", nullable: false),
                    MoTa = table.Column<string>(type: "text", nullable: true),
                    SoTien = table.Column<decimal>(type: "numeric", nullable: false),
                    NguoiDuyet = table.Column<string>(type: "text", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DaXoa = table.Column<bool>(type: "boolean", nullable: false)
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
                table: "CauHinhHeThongs",
                columns: new[] { "Id", "DaXoa", "GhiChu", "NgayCapNhat", "NgayHieuLuc", "NgayTao", "PhuCapAnTruaMoiNgay", "PhuCapXangXeMoiNgay" },
                values: new object[] { 1, false, "Cấu hình mặc định", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 30000m, 20000m });

            migrationBuilder.InsertData(
                table: "ChucVus",
                columns: new[] { "Id", "CapBac", "DaXoa", "LuongToiDa", "LuongToiThieu", "MaChucVu", "NgayCapNhat", "NgayTao", "PhuCapDienThoai", "PhuCapNhaO", "PhuCapTrachNhiem", "TenChucVu" },
                values: new object[,]
                {
                    { 1, 10, false, 200000000m, 50000000m, "CEO", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 500000m, 1000000m, 2000000m, "Giám đốc" },
                    { 2, 7, false, 50000000m, 20000000m, "MGR", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 300000m, 500000m, 1000000m, "Trưởng phòng" }
                });

            migrationBuilder.InsertData(
                table: "PhongBans",
                columns: new[] { "Id", "DaXoa", "MaPhongBan", "MoTa", "NgayCapNhat", "NgayTao", "TenPhongBan", "TruongPhongId" },
                values: new object[,]
                {
                    { 1, false, "IT", null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Phòng Công nghệ Thông tin", null },
                    { 2, false, "HR", null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Phòng Nhân sự", null }
                });

            migrationBuilder.InsertData(
                table: "TaiKhoans",
                columns: new[] { "Id", "DaXoa", "MatKhau", "NgayCapNhat", "NgayTao", "NhanVienId", "TenDangNhap", "VaiTro" },
                values: new object[] { 1, false, "admin123", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "admin", "Admin" });

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
                name: "IX_ChamCongs_NhanVienId_Thang_Nam",
                table: "ChamCongs",
                columns: new[] { "NhanVienId", "Thang", "Nam" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhanViens_ChucVuId",
                table: "NhanViens",
                column: "ChucVuId");

            migrationBuilder.CreateIndex(
                name: "IX_NhanViens_Email",
                table: "NhanViens",
                column: "Email",
                unique: true);

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
                name: "IX_TaiKhoans_NhanVienId",
                table: "TaiKhoans",
                column: "NhanVienId");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoans_TenDangNhap",
                table: "TaiKhoans",
                column: "TenDangNhap",
                unique: true);

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
                name: "CauHinhHeThongs");

            migrationBuilder.DropTable(
                name: "CauHinhLuongs");

            migrationBuilder.DropTable(
                name: "ChamCongs");

            migrationBuilder.DropTable(
                name: "TaiKhoans");

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
