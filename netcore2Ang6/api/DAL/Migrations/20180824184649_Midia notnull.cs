using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Midianotnull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Midias_MidiaId",
                table: "Clientes");

            migrationBuilder.AlterColumn<int>(
                name: "MidiaId",
                table: "Clientes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Midias_MidiaId",
                table: "Clientes",
                column: "MidiaId",
                principalTable: "Midias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Midias_MidiaId",
                table: "Clientes");

            migrationBuilder.AlterColumn<int>(
                name: "MidiaId",
                table: "Clientes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Midias_MidiaId",
                table: "Clientes",
                column: "MidiaId",
                principalTable: "Midias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
