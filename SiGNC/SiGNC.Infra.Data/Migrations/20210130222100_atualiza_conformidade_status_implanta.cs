using Microsoft.EntityFrameworkCore.Migrations;

namespace SiGNC.Infra.Data.Migrations
{
    public partial class atualiza_conformidade_status_implanta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImplantacaoConformidade_StatusConformidade",
                table: "ImplantarConformidade");

            //migrationBuilder.DropIndex(
            //    name: "IX_ImplantarConformidade_StatusConformidadeId",
            //    table: "ImplantarConformidade");

            migrationBuilder.AddColumn<string>(
                name: "NumeroConformidade",
                table: "Conformidade",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusConformidadeId",
                table: "Conformidade",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Conformidade_StatusConformidadeId",
                table: "Conformidade",
                column: "StatusConformidadeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conformidade_StatusConformidade",
                table: "Conformidade",
                column: "StatusConformidadeId",
                principalTable: "StatusConformidade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conformidade_StatusConformidade",
                table: "Conformidade");

            //migrationBuilder.DropIndex(
            //    name: "IX_Conformidade_StatusConformidadeId",
            //    table: "Conformidade");

            migrationBuilder.DropColumn(
                name: "NumeroConformidade",
                table: "Conformidade");

            migrationBuilder.DropColumn(
                name: "StatusConformidadeId",
                table: "Conformidade");

            migrationBuilder.CreateIndex(
                name: "IX_ImplantarConformidade_StatusConformidadeId",
                table: "ImplantarConformidade",
                column: "StatusConformidadeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImplantacaoConformidade_StatusConformidade",
                table: "ImplantarConformidade",
                column: "StatusConformidadeId",
                principalTable: "StatusConformidade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
