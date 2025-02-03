using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CircuitZone.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Produtos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Marcas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Imagens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Categorias",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Marcas");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Imagens");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Categorias");
        }
    }
}
