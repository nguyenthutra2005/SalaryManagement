using Microsoft.EntityFrameworkCore;
using SalaryManagement.Domain.Entities;
using SalaryManagement.Domain.Enums;

namespace SalaryManagement.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<PhongBan> PhongBans { get; set; }
        public DbSet<ChucVu> ChucVus { get; set; }
        public DbSet<CauHinhLuong> CauHinhLuongs { get; set; }
        public DbSet<BangLuong> BangLuongs { get; set; }
        public DbSet<ThuongPhat> ThuongPhats { get; set; }
        public DbSet<ChamCong> ChamCongs { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<CauHinhHeThong> CauHinhHeThongs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Soft delete filter
            modelBuilder.Entity<NhanVien>().HasQueryFilter(e => !e.DaXoa);
            modelBuilder.Entity<PhongBan>().HasQueryFilter(e => !e.DaXoa);

            // Cấu hình NhanVien
            modelBuilder.Entity<NhanVien>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.MaNhanVien).IsRequired().HasMaxLength(20);
                e.Property(x => x.HoTen).IsRequired().HasMaxLength(100);
                e.HasIndex(x => x.MaNhanVien).IsUnique();
                e.HasIndex(x => x.Email).IsUnique();

                e.HasOne(x => x.PhongBan)
                    .WithMany(p => p.NhanViens)
                    .HasForeignKey(x => x.PhongBanId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.ChucVu)
                    .WithMany(c => c.NhanViens)
                    .HasForeignKey(x => x.ChucVuId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Cấu hình CauHinhLuong (quan hệ 1-1)
            modelBuilder.Entity<CauHinhLuong>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasOne(x => x.NhanVien)
                    .WithOne(n => n.CauHinhLuong)
                    .HasForeignKey<CauHinhLuong>(x => x.NhanVienId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.Property(x => x.LuongCoBan).HasColumnType("decimal(18,2)");
                e.Property(x => x.PhuCapAnTrua).HasColumnType("decimal(18,2)");
                e.Property(x => x.PhuCapXangXe).HasColumnType("decimal(18,2)");
                e.Property(x => x.PhuCapTrachNhiem).HasColumnType("decimal(18,2)");
                e.Property(x => x.PhuCapDienThoai).HasColumnType("decimal(18,2)");
                e.Property(x => x.PhuCapNhaO).HasColumnType("decimal(18,2)");
            });

            // Cấu hình BangLuong
            modelBuilder.Entity<BangLuong>(e =>
            {
                e.HasIndex(x => new { x.NhanVienId, x.Thang, x.Nam }).IsUnique();
                e.Property(x => x.LuongCoBan).HasColumnType("decimal(18,2)");
                e.Property(x => x.TongThuNhap).HasColumnType("decimal(18,2)");
                e.Property(x => x.ThuNhapRong).HasColumnType("decimal(18,2)");
                e.Property(x => x.ThueThuNhapCaNhan).HasColumnType("decimal(18,2)");
                e.Property(x => x.SoNgayCongThucTe).HasColumnType("decimal(5,1)");
            });

            // Cấu hình ChamCong
            modelBuilder.Entity<ChamCong>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasIndex(x => new { x.NhanVienId, x.Thang, x.Nam }).IsUnique();
                e.HasOne(x => x.NhanVien)
                    .WithMany()
                    .HasForeignKey(x => x.NhanVienId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Cấu hình TaiKhoan
            modelBuilder.Entity<TaiKhoan>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasIndex(x => x.TenDangNhap).IsUnique();
                e.HasOne(x => x.NhanVien)
                    .WithMany()
                    .HasForeignKey(x => x.NhanVienId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Cấu hình CauHinhHeThong
            modelBuilder.Entity<CauHinhHeThong>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.PhuCapAnTruaMoiNgay).HasColumnType("decimal(18,2)");
                e.Property(x => x.PhuCapXangXeMoiNgay).HasColumnType("decimal(18,2)");
            });

            // Cấu hình ChucVu
            modelBuilder.Entity<ChucVu>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.PhuCapTrachNhiem).HasColumnType("decimal(18,2)");
                e.Property(x => x.PhuCapDienThoai).HasColumnType("decimal(18,2)");
                e.Property(x => x.PhuCapNhaO).HasColumnType("decimal(18,2)");
            });

            // Seed dữ liệu mẫu
            modelBuilder.Entity<PhongBan>().HasData(
                new PhongBan { Id = 1, MaPhongBan = "IT", TenPhongBan = "Phòng Công nghệ Thông tin", NgayTao = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new PhongBan { Id = 2, MaPhongBan = "HR", TenPhongBan = "Phòng Nhân sự", NgayTao = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            );

            modelBuilder.Entity<ChucVu>().HasData(
                new ChucVu
                {
                    Id = 1,
                    MaChucVu = "CEO",
                    TenChucVu = "Giám đốc",
                    CapBac = 10,
                    LuongToiThieu = 50000000,
                    LuongToiDa = 200000000,
                    PhuCapTrachNhiem = 2_000_000,
                    PhuCapDienThoai = 500_000,
                    PhuCapNhaO = 1_000_000,
                    NgayTao = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new ChucVu
                {
                    Id = 2,
                    MaChucVu = "MGR",
                    TenChucVu = "Trưởng phòng",
                    CapBac = 7,
                    LuongToiThieu = 20000000,
                    LuongToiDa = 50000000,
                    PhuCapTrachNhiem = 1_000_000,
                    PhuCapDienThoai = 300_000,
                    PhuCapNhaO = 500_000,
                    NgayTao = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            // Seed cấu hình hệ thống mặc định
            modelBuilder.Entity<CauHinhHeThong>().HasData(
                new CauHinhHeThong
                {
                    Id = 1,
                    PhuCapAnTruaMoiNgay = 30_000m,
                    PhuCapXangXeMoiNgay = 20_000m,
                    NgayHieuLuc = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    GhiChu = "Cấu hình mặc định",
                    NgayTao = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    DaXoa = false
                }
            );

            // Seed tài khoản Admin mặc định
            modelBuilder.Entity<TaiKhoan>().HasData(
                new TaiKhoan
                {
                    Id = 1,
                    TenDangNhap = "admin",
                    MatKhau = "admin123",
                    VaiTro = "Admin",
                    NhanVienId = null,
                    NgayTao = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    DaXoa = false
                }
            );
        }
    }
}