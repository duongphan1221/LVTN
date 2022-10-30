using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace QL_CDC.Models
{
    public partial class QL_CDCContext : DbContext
    {
        public QL_CDCContext()
        {
        }

        public QL_CDCContext(DbContextOptions<QL_CDCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CHAT> CHATs { get; set; }
        public virtual DbSet<CHITIETHOADON> CHITIETHOADONs { get; set; }
        public virtual DbSet<GIOHANG> GIOHANGs { get; set; }
        public virtual DbSet<HINHANH> HINHANHs { get; set; }
        public virtual DbSet<HOADONMUA> HOADONMUAs { get; set; }
        public virtual DbSet<KHUYENMAI> KHUYENMAIs { get; set; }
        public virtual DbSet<LOAIMATHANG> LOAIMATHANGs { get; set; }
        public virtual DbSet<LOAISANPHAM> LOAISANPHAMs { get; set; }
        public virtual DbSet<NHANXETNGUOIBAN> NHANXETNGUOIBANs { get; set; }
        public virtual DbSet<SANPHAM> SANPHAMs { get; set; }
        public virtual DbSet<SINHVIEN> SINHVIENs { get; set; }
        public virtual DbSet<TIENSHIP> TIENSHIPs { get; set; }
        public virtual DbSet<TINHTRANGHOADON> TINHTRANGHOADONs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-TMQS3DR\\SQLEXPRESS;database=QL_CDC;Trusted_Connection=True;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CHAT>(entity =>
            {
                entity.HasKey(e => new { e.SV_MSSV_G, e.SV_MSSV_N, e.CHAT_THOIGIAN });

                entity.ToTable("CHAT");

                entity.HasIndex(e => e.SV_MSSV_N, "CHAT2_FK");

                entity.HasIndex(e => e.SV_MSSV_G, "CHAT_FK");

                entity.Property(e => e.SV_MSSV_G)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SV_MSSV_N)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CHAT_THOIGIAN).HasColumnType("datetime");

                entity.Property(e => e.CHAT_NOIDUNG).HasMaxLength(4000);

                entity.HasOne(d => d.SV_MSSV_GNavigation)
                    .WithMany(p => p.CHATSV_MSSV_GNavigations)
                    .HasForeignKey(d => d.SV_MSSV_G)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHAT_CHAT_SINHVIEN");

                entity.HasOne(d => d.SV_MSSV_NNavigation)
                    .WithMany(p => p.CHATSV_MSSV_NNavigations)
                    .HasForeignKey(d => d.SV_MSSV_N)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHAT_CHAT2_SINHVIEN");
            });

            modelBuilder.Entity<CHITIETHOADON>(entity =>
            {
                entity.HasKey(e => new { e.SP_MSSP, e.HD_MSHD });

                entity.ToTable("CHITIETHOADON");

                entity.HasIndex(e => e.HD_MSHD, "CHITIETHOADON2_FK");

                entity.HasIndex(e => e.SP_MSSP, "CHITIETHOADON_FK");

                entity.Property(e => e.SP_MSSP)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.HD_MSHD)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.HD_MSHDNavigation)
                    .WithMany(p => p.CHITIETHOADONs)
                    .HasForeignKey(d => d.HD_MSHD)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHITIETH_CHITIETHO_HOADONMU");

                entity.HasOne(d => d.SP_MSSPNavigation)
                    .WithMany(p => p.CHITIETHOADONs)
                    .HasForeignKey(d => d.SP_MSSP)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHITIETH_CTHD_SANPHAM");
            });

            modelBuilder.Entity<GIOHANG>(entity =>
            {
                entity.HasKey(e => new { e.SV_MSSV, e.SP_MSSP });

                entity.ToTable("GIOHANG");

                entity.HasIndex(e => e.SV_MSSV, "GIOHANG_FK");

                entity.Property(e => e.SV_MSSV)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SP_MSSP)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.SP_MSSPNavigation)
                    .WithMany(p => p.GIOHANGs)
                    .HasForeignKey(d => d.SP_MSSP)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GIOHANG_CO_SANPHAM");

                entity.HasOne(d => d.SV_MSSVNavigation)
                    .WithMany(p => p.GIOHANGs)
                    .HasForeignKey(d => d.SV_MSSV)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GIOHANG_GIOHANG_SINHVIEN");
            });

            modelBuilder.Entity<HINHANH>(entity =>
            {
                entity.HasKey(e => e.HA_MSHA)
                    .IsClustered(false);

                entity.ToTable("HINHANH");

                entity.HasIndex(e => e.SP_MSSP, "CO_FK");

                entity.HasIndex(e => e.SV_MSSV, "DAT_FK");

                entity.Property(e => e.HA_MSHA)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.HA_LINK)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SP_MSSP)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SV_MSSV)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.SP_MSSPNavigation)
                    .WithMany(p => p.HINHANHs)
                    .HasForeignKey(d => d.SP_MSSP)
                    .HasConstraintName("FK_HINHANH_CUA_SANPHAM");

                entity.HasOne(d => d.SV_MSSVNavigation)
                    .WithMany(p => p.HINHANHs)
                    .HasForeignKey(d => d.SV_MSSV)
                    .HasConstraintName("FK_HINHANH_CUA4_SINHVIEN");
            });

            modelBuilder.Entity<HOADONMUA>(entity =>
            {
                entity.HasKey(e => e.HD_MSHD)
                    .IsClustered(false);

                entity.ToTable("HOADONMUA");

                entity.HasIndex(e => e.TT_MSTT, "CO2_FK");

                entity.HasIndex(e => e.SV_MSSV, "MUA_FK");

                entity.Property(e => e.HD_MSHD)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.HD_DIACHI).HasMaxLength(500);

                entity.Property(e => e.HD_NGAYMUA).HasColumnType("datetime");

                entity.Property(e => e.HD_NGUOINHAN).HasMaxLength(200);

                entity.Property(e => e.HD_SDT)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SV_MSSV)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.SV_MSSVNavigation)
                    .WithMany(p => p.HOADONMUAs)
                    .HasForeignKey(d => d.SV_MSSV)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HOADONMU_CUA2_SINHVIEN");

                entity.HasOne(d => d.TT_MSTTNavigation)
                    .WithMany(p => p.HOADONMUAs)
                    .HasForeignKey(d => d.TT_MSTT)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HOADONMU_CO2_TINHTRAN");
            });

            modelBuilder.Entity<KHUYENMAI>(entity =>
            {
                entity.HasKey(e => e.KM_MSKM)
                    .IsClustered(false);

                entity.ToTable("KHUYENMAI");

                entity.HasIndex(e => e.SP_MSSP, "DUOC_FK");

                entity.Property(e => e.KM_MSKM)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SP_MSSP)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.SP_MSSPNavigation)
                    .WithMany(p => p.KHUYENMAIs)
                    .HasForeignKey(d => d.SP_MSSP)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KHUYENMA_DUOC_SANPHAM");
            });

            modelBuilder.Entity<LOAIMATHANG>(entity =>
            {
                entity.HasKey(e => e.MH_MAMH)
                    .IsClustered(false);

                entity.ToTable("LOAIMATHANG");

                entity.Property(e => e.MH_MAMH).ValueGeneratedNever();

                entity.Property(e => e.MH_TENMH).HasMaxLength(200);
            });

            modelBuilder.Entity<LOAISANPHAM>(entity =>
            {
                entity.HasKey(e => e.LOAI_MALOAI)
                    .IsClustered(false);

                entity.ToTable("LOAISANPHAM");

                entity.HasIndex(e => e.SHIP_MA, "CO4_FK");

                entity.HasIndex(e => e.MH_MAMH, "THUOC2_FK");

                entity.Property(e => e.LOAI_MALOAI).ValueGeneratedNever();

                entity.Property(e => e.LOAI_TENLOAI).HasMaxLength(200);

                entity.HasOne(d => d.MH_MAMHNavigation)
                    .WithMany(p => p.LOAISANPHAMs)
                    .HasForeignKey(d => d.MH_MAMH)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LOAISANP_THUOC2_LOAIMATH");

                entity.HasOne(d => d.SHIP_MANavigation)
                    .WithMany(p => p.LOAISANPHAMs)
                    .HasForeignKey(d => d.SHIP_MA)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LOAISANP_CO4_TIENSHIP");
            });

            modelBuilder.Entity<NHANXETNGUOIBAN>(entity =>
            {
                entity.HasKey(e => new { e.SV_MSSV_M, e.SV_MSSV_B });

                entity.ToTable("NHANXETNGUOIBAN");

                entity.HasIndex(e => e.SV_MSSV_M, "BINHLUAN_FKA");

                entity.HasIndex(e => e.SV_MSSV_B, "NHANXETNGUOIBAN2_FK");

                entity.Property(e => e.SV_MSSV_M)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SV_MSSV_B)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.NX_NGAY).HasColumnType("datetime");

                entity.Property(e => e.NX_NOIDUNG).HasMaxLength(2000);

                entity.HasOne(d => d.SV_MSSV_BNavigation)
                    .WithMany(p => p.NHANXETNGUOIBANSV_MSSV_BNavigations)
                    .HasForeignKey(d => d.SV_MSSV_B)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NHANXETN_NHANXETNG_SINHVIEN");

                entity.HasOne(d => d.SV_MSSV_MNavigation)
                    .WithMany(p => p.NHANXETNGUOIBANSV_MSSV_MNavigations)
                    .HasForeignKey(d => d.SV_MSSV_M)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NHANXETN_CUA3_SINHVIEN");
            });

            modelBuilder.Entity<SANPHAM>(entity =>
            {
                entity.HasKey(e => e.SP_MSSP)
                    .IsClustered(false);

                entity.ToTable("SANPHAM");

                entity.HasIndex(e => e.SV_MSSV, "BAN_FK");

                entity.Property(e => e.SP_MSSP)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SP_HANGSX).HasMaxLength(200);

                entity.Property(e => e.SP_MOTA).HasMaxLength(4000);

                entity.Property(e => e.SP_NGAYDANG).HasColumnType("datetime");

                entity.Property(e => e.SP_TENSP).HasMaxLength(200);

                entity.Property(e => e.SV_MSSV)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.LOAI_MALOAINavigation)
                    .WithMany(p => p.SANPHAMs)
                    .HasForeignKey(d => d.LOAI_MALOAI)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SANPHAM_THUOC_LOAISANP");

                entity.HasOne(d => d.SV_MSSVNavigation)
                    .WithMany(p => p.SANPHAMs)
                    .HasForeignKey(d => d.SV_MSSV)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SANPHAM_DO_SINHVIEN");
            });

            modelBuilder.Entity<SINHVIEN>(entity =>
            {
                entity.HasKey(e => e.SV_MSSV)
                    .IsClustered(false);

                entity.ToTable("SINHVIEN");

                entity.Property(e => e.SV_MSSV)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SV_DIACHIGIAOHANG).HasMaxLength(500);

                entity.Property(e => e.SV_EMAIL).HasMaxLength(200);

                entity.Property(e => e.SV_HOTEN).HasMaxLength(200);

                entity.Property(e => e.SV_LANHDCUOI).HasColumnType("datetime");

                entity.Property(e => e.SV_MATKHAU)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SV_NGAYTAOTK).HasColumnType("datetime");

                entity.Property(e => e.SV_SDT)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SV_TENHIENTHI).HasMaxLength(200);
            });

            modelBuilder.Entity<TIENSHIP>(entity =>
            {
                entity.HasKey(e => e.SHIP_MA)
                    .IsClustered(false);

                entity.ToTable("TIENSHIP");

                entity.Property(e => e.SHIP_MA).ValueGeneratedNever();
            });

            modelBuilder.Entity<TINHTRANGHOADON>(entity =>
            {
                entity.HasKey(e => e.TT_MSTT)
                    .IsClustered(false);

                entity.ToTable("TINHTRANGHOADON");

                entity.Property(e => e.TT_MSTT).ValueGeneratedNever();

                entity.Property(e => e.TT_TRANGTHAI).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
