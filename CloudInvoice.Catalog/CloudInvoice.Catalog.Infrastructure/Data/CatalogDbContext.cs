using CloudInvoice.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudInvoice.Catalog.Infrastructure.Data
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.BasePrice).HasPrecision(18, 2);
                entity.Property(p => p.TaxRate).HasPrecision(5, 2);
                entity.Property(p => p.UnitOfMeasure).HasConversion<string>().HasMaxLength(20);
            });
        }
    }
}
