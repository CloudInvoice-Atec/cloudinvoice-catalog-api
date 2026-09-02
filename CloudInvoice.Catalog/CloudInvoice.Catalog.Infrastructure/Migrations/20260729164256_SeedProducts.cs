using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CloudInvoice.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BasePrice", "CategoryId", "Code", "Description", "IsActive", "TaxRate", "UnitOfMeasure" },
                values: new object[,]
                {
                    { new Guid("a0000001-0000-0000-0000-000000000001"), 45.90m, new Guid("22222222-2222-2222-2222-222222222222"), "P001", "Teclado Mecânico RGB", true, 23m, "Unidade" },
                    { new Guid("a0000001-0000-0000-0000-000000000002"), 15.50m, new Guid("22222222-2222-2222-2222-222222222222"), "P002", "Rato sem Fios", true, 23m, "Unidade" },
                    { new Guid("a0000001-0000-0000-0000-000000000003"), 129.99m, new Guid("22222222-2222-2222-2222-222222222222"), "P003", "Monitor 24 Polegadas", true, 23m, "Unidade" },
                    { new Guid("a0000001-0000-0000-0000-000000000004"), 8.50m, new Guid("33333333-3333-3333-3333-333333333333"), "P004", "Café em Grão 1kg", true, 6m, "Kg" },
                    { new Guid("a0000001-0000-0000-0000-000000000005"), 6.20m, new Guid("33333333-3333-3333-3333-333333333333"), "P005", "Azeite Extra Virgem 1L", true, 6m, "Litro" },
                    { new Guid("a0000001-0000-0000-0000-000000000006"), 12.00m, new Guid("44444444-4444-4444-4444-444444444444"), "P006", "T-shirt Algodão", true, 23m, "Unidade" },
                    { new Guid("a0000001-0000-0000-0000-000000000007"), 35.00m, new Guid("44444444-4444-4444-4444-444444444444"), "P007", "Calças de Ganga", false, 23m, "Unidade" },
                    { new Guid("a0000001-0000-0000-0000-000000000008"), 45.00m, new Guid("55555555-5555-5555-5555-555555555555"), "P008", "Consultoria Informática", true, 23m, "Hora" },
                    { new Guid("a0000001-0000-0000-0000-000000000009"), 150.00m, new Guid("55555555-5555-5555-5555-555555555555"), "P009", "Instalação de Rede", true, 23m, "Unidade" },
                    { new Guid("a0000001-0000-0000-0000-000000000010"), 25.00m, new Guid("11111111-1111-1111-1111-111111111111"), "P010", "Fita Adesiva", true, 23m, "Metro" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a0000001-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a0000001-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a0000001-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a0000001-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a0000001-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a0000001-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a0000001-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a0000001-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a0000001-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a0000001-0000-0000-0000-000000000010"));
        }
    }
}
