using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDato.Migrations
{
    /// <inheritdoc />
    public partial class TiposDeCapitulo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Contador",
                table: "Capitulos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoCapitulo",
                table: "Capitulos",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Opcion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapituloDestinoId = table.Column<int>(type: "int", nullable: false),
                    CapituloIntermedioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opcion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opcion_Capitulos_CapituloDestinoId",
                        column: x => x.CapituloDestinoId,
                        principalTable: "Capitulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Opcion_Capitulos_CapituloIntermedioId",
                        column: x => x.CapituloIntermedioId,
                        principalTable: "Capitulos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Opcion_CapituloDestinoId",
                table: "Opcion",
                column: "CapituloDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Opcion_CapituloIntermedioId",
                table: "Opcion",
                column: "CapituloIntermedioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Opcion");

            migrationBuilder.DropColumn(
                name: "Contador",
                table: "Capitulos");

            migrationBuilder.DropColumn(
                name: "TipoCapitulo",
                table: "Capitulos");
        }
    }
}
