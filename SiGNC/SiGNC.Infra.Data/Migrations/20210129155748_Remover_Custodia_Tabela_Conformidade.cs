using Microsoft.EntityFrameworkCore.Migrations;

namespace SiGNC.Infra.Data.Migrations
{
    public partial class Remover_Custodia_Tabela_Conformidade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConformidadeId",
                table: "CausaRaizConformidade");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConformidadeId",
                table: "CausaRaizConformidade",
                type: "int",
                nullable: true);
        }
    }
}
