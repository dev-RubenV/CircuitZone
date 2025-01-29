using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CircuitZone.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeMarca = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoMovimentos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMovimentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimeiroNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UltimoNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarcaId = table.Column<int>(type: "int", nullable: false),
                    MarcaId1 = table.Column<long>(type: "bigint", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CodigoEAN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantidadeDisponivel = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    CategoriaId1 = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_CategoriaId1",
                        column: x => x.CategoriaId1,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produtos_Marcas_MarcaId1",
                        column: x => x.MarcaId1,
                        principalTable: "Marcas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Imagens",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId1 = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Imagens_Produtos_ProdutoId1",
                        column: x => x.ProdutoId1,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movimentos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    DataMovimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoMovimentoId = table.Column<int>(type: "int", nullable: false),
                    TipoMovimentoId1 = table.Column<long>(type: "bigint", nullable: false),
                    UtilizadorId = table.Column<int>(type: "int", nullable: false),
                    UtilizadorId1 = table.Column<long>(type: "bigint", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId1 = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimentos_Produtos_ProdutoId1",
                        column: x => x.ProdutoId1,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimentos_TipoMovimentos_TipoMovimentoId1",
                        column: x => x.TipoMovimentoId1,
                        principalTable: "TipoMovimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimentos_Utilizadores_UtilizadorId1",
                        column: x => x.UtilizadorId1,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Imagens_ProdutoId1",
                table: "Imagens",
                column: "ProdutoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentos_ProdutoId1",
                table: "Movimentos",
                column: "ProdutoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentos_TipoMovimentoId1",
                table: "Movimentos",
                column: "TipoMovimentoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentos_UtilizadorId1",
                table: "Movimentos",
                column: "UtilizadorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaId1",
                table: "Produtos",
                column: "CategoriaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_MarcaId1",
                table: "Produtos",
                column: "MarcaId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Imagens");

            migrationBuilder.DropTable(
                name: "Movimentos");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "TipoMovimentos");

            migrationBuilder.DropTable(
                name: "Utilizadores");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Marcas");
        }
    }
}
