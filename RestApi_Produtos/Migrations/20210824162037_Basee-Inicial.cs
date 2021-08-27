using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestApi_Produtos.Migrations
{
    public partial class BaseeInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Estoque",
                columns: table => new
                {
                    Codigo_Estoque = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Movimentacao = table.Column<string>(nullable: true),
                    Nome_Produto = table.Column<string>(nullable: true),
                    Quantidade = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Estoque", x => x.Codigo_Estoque);
                });

            migrationBuilder.CreateTable(
                name: "TB_Produto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    Valor = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Produto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_USUARIO",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USUARIO", x => x.Codigo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_Estoque");

            migrationBuilder.DropTable(
                name: "TB_Produto");

            migrationBuilder.DropTable(
                name: "TB_USUARIO");
        }
    }
}
