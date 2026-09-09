using CloudInvoice.Catalog.Domain.Entities;
using CloudInvoice.Catalog.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CloudInvoice.Catalog.Infrastructure.Data
{
    public static class ProductSeeder
    {
        private static readonly Guid CategoriaGeralId = new("11111111-1111-1111-1111-111111111111");
        private static readonly Guid CategoriaEletronicaId = new("22222222-2222-2222-2222-222222222222");
        private static readonly Guid CategoriaAlimentacaoId = new("33333333-3333-3333-3333-333333333333");
        private static readonly Guid CategoriaVestuarioId = new("44444444-4444-4444-4444-444444444444");
        private static readonly Guid CategoriaServicosId = new("55555555-5555-5555-5555-555555555555");

        private static readonly (Guid CategoryId, string[] Names, decimal MinPrice, decimal MaxPrice, decimal TaxRate, UnitOfMeasure Unit)[] Templates =
        {
            (CategoriaEletronicaId,
                new[] { "Monitor", "Rato", "Teclado", "Auscultadores", "Impressora", "Router", "Webcam",
                        "Disco Externo", "Coluna Bluetooth", "Carregador USB-C", "Powerbank", "Hub USB",
                        "Cabo HDMI", "Leitor de Cartões", "Microfone", "Suporte para Portátil",
                        "Ventoinha USB", "Adaptador Wi-Fi", "Placa de Rede", "Regador Cabos" },
                9.99m, 249.99m, 23m, UnitOfMeasure.Unidade),

            (CategoriaAlimentacaoId,
                new[] { "Café", "Chá", "Massa", "Arroz", "Azeite", "Vinho", "Chocolate", "Bolachas",
                        "Mel", "Compota", "Cereais", "Farinha", "Açúcar", "Sal Marinho", "Especiarias",
                        "Fruta Seca", "Vinagre Balsâmico", "Molho de Tomate", "Sumo Natural", "Água Mineral" },
                1.50m, 15.00m, 6m, UnitOfMeasure.Kg),

            (CategoriaVestuarioId,
                new[] { "T-shirt", "Calças", "Casaco", "Sapatilhas", "Camisa", "Boné", "Sweatshirt",
                        "Meias", "Cachecol", "Cinto", "Vestido", "Saia", "Calções", "Luvas",
                        "Blazer", "Colete", "Gravata", "Pijama", "Camisola de Malha", "Bota" },
                8.00m, 79.99m, 23m, UnitOfMeasure.Unidade),

            (CategoriaServicosId,
                new[] { "Consultoria", "Formação", "Manutenção", "Suporte Técnico", "Instalação",
                        "Configuração de Rede", "Auditoria Informática", "Backup na Cloud",
                        "Desenvolvimento à Medida", "Gestão de Sistemas", "Análise de Dados",
                        "Migração de Servidores", "Testes de Segurança", "Formação em Excel",
                        "Suporte Remoto", "Otimização de Website", "SEO Técnico",
                        "Consultoria em RGPD", "Recuperação de Dados", "Manutenção Preventiva" },
                20.00m, 90.00m, 23m, UnitOfMeasure.Hora),

            (CategoriaGeralId,
                new[] { "Caixa de Arquivo", "Fita Adesiva", "Resma de Papel A4", "Caneta Esferográfica",
                        "Pasta de Argolas", "Marcador Permanente", "Agrafador", "Clips",
                        "Bloco de Notas", "Envelope A4", "Post-it", "Tesoura", "Cola em Bastão",
                        "Separadores", "Etiquetas Adesivas", "Elásticos", "Perfurador",
                        "Corretor Líquido", "Lápis HB", "Borracha" },
                0.50m, 20.00m, 23m, UnitOfMeasure.Unidade),
        };

        public static async Task SeedAsync(CatalogDbContext context)
        {
            var existingCount = await context.Products.CountAsync();
            if (existingCount >= 100)
            {
                return; // já tem volume suficiente, não gera mais
            }

            var random = new Random(123); // seed fixa - resultados sempre iguais entre arranques
            var products = new List<Product>();
            var codeCounter = 1;

            var perTemplateTarget = (100 / Templates.Length) + 1;

            foreach (var template in Templates)
            {
                for (var i = 0; i < perTemplateTarget; i++)
                {
                    var name = template.Names[i % template.Names.Length];
                    var variant = i / template.Names.Length;
                    var description = variant == 0 ? name : $"{name} (Modelo {variant + 1})";

                    var priceRange = (double)(template.MaxPrice - template.MinPrice);
                    var price = Math.Round((decimal)(random.NextDouble() * priceRange) + template.MinPrice, 2);

                    products.Add(new Product
                    {
                        Id = Guid.NewGuid(),
                        Code = $"S{codeCounter:D3}",
                        Description = description,
                        BasePrice = price,
                        TaxRate = template.TaxRate,
                        UnitOfMeasure = template.Unit,
                        IsActive = random.Next(0, 10) != 0, // ~10% inativos, para testar filtros de estado
                        CategoryId = template.CategoryId
                    });

                    codeCounter++;
                }
            }

            // Garante pelo menos 100 no total (corta o excedente do arredondamento)
            var needed = 100 - existingCount;
            if (products.Count > needed)
            {
                products = products.Take(needed).ToList();
            }

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
}