using CloudInvoice.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudInvoice.Catalog.Infrastructure.Data
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        // IDs fixos - necessário para o seed (HasData) ser sempre determinístico entre migrations
        private static readonly Guid CategoriaGeralId = new("11111111-1111-1111-1111-111111111111");
        private static readonly Guid CategoriaEletronicaId = new("22222222-2222-2222-2222-222222222222");
        private static readonly Guid CategoriaAlimentacaoId = new("33333333-3333-3333-3333-333333333333");
        private static readonly Guid CategoriaVestuarioId = new("44444444-4444-4444-4444-444444444444");
        private static readonly Guid CategoriaServicosId = new("55555555-5555-5555-5555-555555555555");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.BasePrice).HasPrecision(18, 2);
                entity.Property(p => p.TaxRate).HasPrecision(5, 2);
                entity.Property(p => p.UnitOfMeasure).HasConversion<string>().HasMaxLength(20);

                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(c => c.Name).HasMaxLength(50).IsRequired();

                entity.HasData(
                    new Category { Id = CategoriaGeralId, Name = "Geral" },
                    new Category { Id = CategoriaEletronicaId, Name = "Eletrónica" },
                    new Category { Id = CategoriaAlimentacaoId, Name = "Alimentação" },
                    new Category { Id = CategoriaVestuarioId, Name = "Vestuário" },
                    new Category { Id = CategoriaServicosId, Name = "Serviços" }
                );
            });
        }
    }
}