using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDato.Migrations
{
    /// <inheritdoc />
    public partial class OpcionesCapitulos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opcion_Capitulos_CapituloDestinoId",
                table: "Opcion");

            migrationBuilder.AddForeignKey(
                name: "FK_Opcion_Capitulos_CapituloDestinoId",
                table: "Opcion",
                column: "CapituloDestinoId",
                principalTable: "Capitulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opcion_Capitulos_CapituloDestinoId",
                table: "Opcion");

            migrationBuilder.AddForeignKey(
                name: "FK_Opcion_Capitulos_CapituloDestinoId",
                table: "Opcion",
                column: "CapituloDestinoId",
                principalTable: "Capitulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
