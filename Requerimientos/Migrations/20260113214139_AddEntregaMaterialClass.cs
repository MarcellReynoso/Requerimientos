using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Requerimientos.Migrations
{
    /// <inheritdoc />
    public partial class AddEntregaMaterialClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntregaMaterial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    CantidadEntregada = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Material = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Retorno = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    HoraSalida = table.Column<TimeOnly>(type: "time", nullable: true),
                    HoraIngreso = table.Column<TimeOnly>(type: "time", nullable: true),
                    FechaRetorno = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    AQuienSeEntregaId = table.Column<int>(type: "int", nullable: false),
                    ResponsableId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntregaMaterial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntregaMaterial_AQuienSeEntrega_Trabajador",
                        column: x => x.AQuienSeEntregaId,
                        principalTable: "Trabajador",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EntregaMaterial_Producto",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EntregaMaterial_Responsable_Trabajador",
                        column: x => x.ResponsableId,
                        principalTable: "Trabajador",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntregaMaterial_AQuienSeEntregaId",
                table: "EntregaMaterial",
                column: "AQuienSeEntregaId");

            migrationBuilder.CreateIndex(
                name: "IX_EntregaMaterial_ProductoId",
                table: "EntregaMaterial",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_EntregaMaterial_ResponsableId",
                table: "EntregaMaterial",
                column: "ResponsableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntregaMaterial");
        }
    }
}
