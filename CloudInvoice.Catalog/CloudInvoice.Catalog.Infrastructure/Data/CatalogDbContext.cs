using CloudInvoice.Catalog.Domain.Entities;
using CloudInvoice.Catalog.Domain.Enums;
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
                entity.HasData(
                    new Product
                    {
                        Id = new Guid("a0000001-0000-0000-0000-000000000001"),
                        Code = "P001",
                        Description = "Teclado Mecânico RGB",
                        BasePrice = 45.90m,
                        TaxRate = 23m,
                        UnitOfMeasure = UnitOfMeasure.Unidade,
                        IsActive = true,
                        CategoryId = CategoriaEletronicaId
                    },
                    new Product
                    {
                        Id = new Guid("a0000001-0000-0000-0000-000000000002"),
                        Code = "P002",
                        Description = "Rato sem Fios",
                        BasePrice = 15.50m,
                        TaxRate = 23m,
                        UnitOfMeasure = UnitOfMeasure.Unidade,
                        IsActive = true,
                        CategoryId = CategoriaEletronicaId
                    },
                    new Product
                    {
                        Id = new Guid("a0000001-0000-0000-0000-000000000003"),
                        Code = "P003",
                        Description = "Monitor 24 Polegadas",
                        BasePrice = 129.99m,
                        TaxRate = 23m,
                        UnitOfMeasure = UnitOfMeasure.Unidade,
                        IsActive = true,
                        CategoryId = CategoriaEletronicaId
                    },
                    new Product
                    {
                        Id = new Guid("a0000001-0000-0000-0000-000000000004"),
                        Code = "P004",
                        Description = "Café em Grão 1kg",
                        BasePrice = 8.50m,
                        TaxRate = 6m,
                        UnitOfMeasure = UnitOfMeasure.Kg,
                        IsActive = true,
                        CategoryId = CategoriaAlimentacaoId
                    },
                    new Product
                    {
                        Id = new Guid("a0000001-0000-0000-0000-000000000005"),
                        Code = "P005",
                        Description = "Azeite Extra Virgem 1L",
                        BasePrice = 6.20m,
                        TaxRate = 6m,
                        UnitOfMeasure = UnitOfMeasure.Litro,
                        IsActive = true,
                        CategoryId = CategoriaAlimentacaoId
                    },
                    new Product
                    {
                        Id = new Guid("a0000001-0000-0000-0000-000000000006"),
                        Code = "P006",
                        Description = "T-shirt Algodão",
                        BasePrice = 12.00m,
                        TaxRate = 23m,
                        UnitOfMeasure = UnitOfMeasure.Unidade,
                        IsActive = true,
                        CategoryId = CategoriaVestuarioId
                    },
                    new Product
                    {
                        Id = new Guid("a0000001-0000-0000-0000-000000000007"),
                        Code = "P007",
                        Description = "Calças de Ganga",
                        BasePrice = 35.00m,
                        TaxRate = 23m,
                        UnitOfMeasure = UnitOfMeasure.Unidade,
                        IsActive = false,
                        CategoryId = CategoriaVestuarioId
                    },
                    new Product
                    {
                        Id = new Guid("a0000001-0000-0000-0000-000000000008"),
                        Code = "P008",
                        Description = "Consultoria Informática",
                        BasePrice = 45.00m,
                        TaxRate = 23m,
                        UnitOfMeasure = UnitOfMeasure.Hora,
                        IsActive = true,
                        CategoryId = CategoriaServicosId
                    },
                    new Product
                    {
                        Id = new Guid("a0000001-0000-0000-0000-000000000009"),
                        Code = "P009",
                        Description = "Instalação de Rede",
                        BasePrice = 150.00m,
                        TaxRate = 23m,
                        UnitOfMeasure = UnitOfMeasure.Unidade,
                        IsActive = true,
                        CategoryId = CategoriaServicosId
                    },
                    new Product
                    {
                        Id = new Guid("a0000001-0000-0000-0000-000000000010"),
                        Code = "P010",
                        Description = "Fita Adesiva",
                        BasePrice = 25.00m,
                        TaxRate = 23m,
                        UnitOfMeasure = UnitOfMeasure.Metro,
                        IsActive = true,
                        CategoryId = CategoriaGeralId
                    }
                );
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