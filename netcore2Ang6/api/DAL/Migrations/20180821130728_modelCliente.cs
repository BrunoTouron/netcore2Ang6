using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DAL.Migrations
{
    public partial class modelCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Idade = table.Column<int>(nullable: false),
                    Sexo = table.Column<string>(maxLength: 15, nullable: true),
                    Endereco = table.Column<string>(maxLength: 100, nullable: true),
                    Cep = table.Column<string>(maxLength: 10, nullable: true),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    CPF = table.Column<string>(maxLength: 15, nullable: true),
                    Profissao = table.Column<string>(maxLength: 30, nullable: true),
                    TelCel = table.Column<string>(maxLength: 16, nullable: true),
                    TelRes = table.Column<string>(maxLength: 16, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    Instagram = table.Column<string>(maxLength: 50, nullable: true),
                    Observacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
