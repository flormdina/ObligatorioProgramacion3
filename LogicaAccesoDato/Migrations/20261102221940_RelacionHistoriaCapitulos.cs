using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDato.Migrations
{
    /// <inheritdoc />
    public partial class RelacionHistoriaCapitulos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HistoriaId",
                table: "Capitulos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Capitulos_HistoriaId",
                table: "Capitulos",
                column: "HistoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Capitulos_Historias_HistoriaId",
                table: "Capitulos",
                column: "HistoriaId",
                principalTable: "Historias",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Capitulos_Historias_HistoriaId",
                table: "Capitulos");

            migrationBuilder.DropIndex(
                name: "IX_Capitulos_HistoriaId",
                table: "Capitulos");

            migrationBuilder.DropColumn(
                name: "HistoriaId",
                table: "Capitulos");
        }
    }
}
