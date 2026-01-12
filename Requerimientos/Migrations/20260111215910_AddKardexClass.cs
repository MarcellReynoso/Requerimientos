using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Requerimientos.Migrations
{
    /// <inheritdoc />
    public partial class AddKardexClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kardex",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Proviene = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrecioUnitario = table.Column<decimal>(type: "money", nullable: true),
                    PrecioVenta = table.Column<decimal>(type: "money", nullable: true),
                    DocumentoIngreso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentoSalida = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    PerteneceId = table.Column<int>(type: "int", nullable: false),
                    QuienRecibeId = table.Column<int>(type: "int", nullable: false),
                    QuienEntregaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kardex", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kardex_Pertenece",
                        column: x => x.PerteneceId,
                        principalTable: "Pertenece",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Kardex_Producto",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Kardex_QuienEntrega_Trabajador",
                        column: x => x.QuienEntregaId,
                        principalTable: "Trabajador",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Kardex_QuienRecibe_Trabajador",
                        column: x => x.QuienRecibeId,
                        principalTable: "Trabajador",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Unidad_Simbolo",
                table: "Unidad",
                column: "Simbolo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kardex_PerteneceId",
                table: "Kardex",
                column: "PerteneceId");

            migrationBuilder.CreateIndex(
                name: "IX_Kardex_ProductoId",
                table: "Kardex",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Kardex_QuienEntregaId",
                table: "Kardex",
                column: "QuienEntregaId");

            migrationBuilder.CreateIndex(
                name: "IX_Kardex_QuienRecibeId",
                table: "Kardex",
                column: "QuienRecibeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kardex");

            migrationBuilder.DropIndex(
                name: "IX_Unidad_Simbolo",
                table: "Unidad");
        }
    }
}
