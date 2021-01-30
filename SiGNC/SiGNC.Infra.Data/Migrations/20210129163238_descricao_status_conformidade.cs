using Microsoft.EntityFrameworkCore.Migrations;

namespace SiGNC.Infra.Data.Migrations
{
    public partial class descricao_status_conformidade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "StatusConformidade",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "StatusConformidade");
        }
    }
}
