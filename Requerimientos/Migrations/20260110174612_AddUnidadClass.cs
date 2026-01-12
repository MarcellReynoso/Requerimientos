using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Requerimientos.Migrations
{
    /// <inheritdoc />
    public partial class AddUnidadClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unidad",
                table: "Producto");

            migrationBuilder.AddColumn<int>(
                name: "UnidadId",
                table: "Producto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Unidad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidad", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Producto_UnidadId",
                table: "Producto",
                column: "UnidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Unidad",
                table: "Producto",
                column: "UnidadId",
                principalTable: "Unidad",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Unidad",
                table: "Producto");

            migrationBuilder.DropTable(
                name: "Unidad");

            migrationBuilder.DropIndex(
                name: "IX_Producto_UnidadId",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "UnidadId",
                table: "Producto");

            migrationBuilder.AddColumn<string>(
                name: "Unidad",
                table: "Producto",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true);
        }
    }
}
