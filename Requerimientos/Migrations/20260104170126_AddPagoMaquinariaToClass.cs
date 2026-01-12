using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Requerimientos.Migrations
{
    /// <inheritdoc />
    public partial class AddPagoMaquinariaToClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EsPagable",
                table: "Clase",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "HorasMinimas1",
                table: "Clase",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HorasMinimas2",
                table: "Clase",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioHora1",
                table: "Clase",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioHora2",
                table: "Clase",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TieneHorometro2",
                table: "Clase",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsPagable",
                table: "Clase");

            migrationBuilder.DropColumn(
                name: "HorasMinimas1",
                table: "Clase");

            migrationBuilder.DropColumn(
                name: "HorasMinimas2",
                table: "Clase");

            migrationBuilder.DropColumn(
                name: "PrecioHora1",
                table: "Clase");

            migrationBuilder.DropColumn(
                name: "PrecioHora2",
                table: "Clase");

            migrationBuilder.DropColumn(
                name: "TieneHorometro2",
                table: "Clase");
        }
    }
}
