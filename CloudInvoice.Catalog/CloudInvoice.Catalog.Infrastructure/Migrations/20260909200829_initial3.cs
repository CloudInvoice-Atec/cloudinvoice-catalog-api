using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CloudInvoice.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Geral" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Eletrónica" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Alimentação" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Vestuário" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "Serviços" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
