using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class fkmidiascliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MidiaId",
                table: "Clientes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_MidiaId",
                table: "Clientes",
                column: "MidiaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Midias_MidiaId",
                table: "Clientes",
                column: "MidiaId",
                principalTable: "Midias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Midias_MidiaId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_MidiaId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "MidiaId",
                table: "Clientes");
        }
    }
}
