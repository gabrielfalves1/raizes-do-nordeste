using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace raizes_do_nordeste.Migrations
{
    /// <inheritdoc />
    public partial class SeedUnidadesEProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Descricao", "Nome" },
                values: new object[] { new Guid("55555555-5555-5555-5555-555555555555"), "Comidas típicas nordestinas servidas a qualquer hora do dia.", "Pratos Regionais" });

            migrationBuilder.InsertData(
                table: "Unidades",
                columns: new[] { "Id", "Cidade", "Endereco", "Estado", "Nome", "Status", "Telefone" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Recife", "Av. Boa Viagem, 123", "PE", "Raízes do Nordeste - Matriz Boa Viagem", 0, "81999990000" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Recife", "Av. República do Líbano, 251", "PE", "Raízes do Nordeste - Shopping Riomar", 0, "81999991111" }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Ativo", "CategoriaId", "Descricao", "Nome", "Preco" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), true, new Guid("55555555-5555-5555-5555-555555555555"), "Cuscuz de milho no capricho com charque desfiada e queijo coalho.", "Cuscuz Completo com Charque", 25.90m },
                    { new Guid("44444444-4444-4444-4444-444444444444"), true, new Guid("55555555-5555-5555-5555-555555555555"), "Tapioca rendada na manteiga de garrafa com carne de sol e nata.", "Tapioca de Carne de Sol", 19.50m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Unidades",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Unidades",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));
        }
    }
}
