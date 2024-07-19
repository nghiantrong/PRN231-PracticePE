using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Repository.Models
{
    public partial class WatercolorsPainting2024DBContext : DbContext
    {
        public WatercolorsPainting2024DBContext()
        {
        }

        public WatercolorsPainting2024DBContext(DbContextOptions<WatercolorsPainting2024DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Style> Styles { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<WatercolorsPainting> WatercolorsPaintings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Style>(entity =>
            {
                entity.ToTable("Style");

                entity.Property(e => e.StyleId).HasMaxLength(30);

                entity.Property(e => e.OriginalCountry).HasMaxLength(160);

                entity.Property(e => e.StyleDescription)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.StyleName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.ToTable("UserAccount");

                entity.HasIndex(e => e.UserEmail, "UQ__UserAcco__08638DF8852CF767")
                    .IsUnique();

                entity.Property(e => e.UserAccountId)
                    .ValueGeneratedNever()
                    .HasColumnName("UserAccountID");

                entity.Property(e => e.UserEmail).HasMaxLength(90);

                entity.Property(e => e.UserFullName)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<WatercolorsPainting>(entity =>
            {
                entity.HasKey(e => e.PaintingId)
                    .HasName("PK__Watercol__CF2D90F2BE6C4BB1");

                entity.ToTable("WatercolorsPainting");

                entity.Property(e => e.PaintingId).HasMaxLength(45);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PaintingAuthor).HasMaxLength(120);

                entity.Property(e => e.PaintingDescription).HasMaxLength(250);

                entity.Property(e => e.PaintingName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.StyleId).HasMaxLength(30);

                entity.HasOne(d => d.Style)
                    .WithMany(p => p.WatercolorsPaintings)
                    .HasForeignKey(d => d.StyleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Watercolo__Style__29572725");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
