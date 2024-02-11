using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public partial class ProductCatalogContext(DbContextOptions<ProductCatalogContext> options) : DbContext(options)
{
    public virtual DbSet<CategoryEntity> Categories { get; set; }

    public virtual DbSet<ManufacturerEntity> Manufacturers { get; set; }

    public virtual DbSet<PriceEntity> Prices { get; set; }

    public virtual DbSet<ProductEntity> Products { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC07FFC707B4");

            entity.HasIndex(e => e.CategoryName, "UQ__Categori__8517B2E07B7C0B95").IsUnique();

            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<ManufacturerEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Manufact__3214EC0710A0E8C9");

            entity.ToTable("Manufacturer");

            entity.HasIndex(e => e.Manufacturer, "UQ__Manufact__D194335A33173037").IsUnique();

            entity.Property(e => e.Manufacturer)
                .HasMaxLength(200)
                .HasColumnName("Manufacturer");
        });

        modelBuilder.Entity<PriceEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Prices__3214EC0759A67044");

            entity.Property(e => e.UnitPrice).HasColumnType("money");
        });

        modelBuilder.Entity<ProductEntity>(entity =>
        {
            entity.HasKey(e => e.ArticleNumber).HasName("PK__Products__3C9911434AEABFFC");

            entity.Property(e => e.ArticleNumber).HasMaxLength(250);
            entity.Property(e => e.Title).HasMaxLength(250);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Catego__3E52440B");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products)
                .HasForeignKey(d => d.ManufacturerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Manufa__3F466844");

            entity.HasOne(d => d.Price).WithMany(p => p.Products)
                .HasForeignKey(d => d.PriceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__PriceI__403A8C7D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
