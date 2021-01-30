using Microsoft.EntityFrameworkCore.Migrations;

namespace SiGNC.Infra.Data.Migrations
{
    public partial class Modificacao_ConfHasCausaRaiz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "ConformidadeHasCausaRaiz",
                newName: "Quais");

            migrationBuilder.AddColumn<bool>(
                name: "Ocorreu",
                table: "ConformidadeHasCausaRaiz",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ocorreu",
                table: "ConformidadeHasCausaRaiz");

            migrationBuilder.RenameColumn(
                name: "Quais",
                table: "ConformidadeHasCausaRaiz",
                newName: "Descricao");
        }
    }
}
