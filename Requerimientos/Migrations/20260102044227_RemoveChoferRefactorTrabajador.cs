using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Requerimientos.Migrations
{
    /// <inheritdoc />
    public partial class RemoveChoferRefactorTrabajador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimiento_Chofer",
                table: "Movimiento");

            migrationBuilder.DropTable(
                name: "Chofer");

            migrationBuilder.DropColumn(
                name: "TotalMinutos",
                table: "Movimiento");

            migrationBuilder.CreateTable(
                name: "Combustible",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    ClaseId = table.Column<int>(type: "int", nullable: false),
                    GlnsEntregado = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ResponsableId = table.Column<int>(type: "int", nullable: false),
                    QuienRecibeId = table.Column<int>(type: "int", nullable: false),
                    HoraEntrega = table.Column<TimeOnly>(type: "time", nullable: false),
                    Litros = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    GlnsXLitro = table.Column<decimal>(type: "decimal(10,6)", nullable: false),
                    TotalGalones = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    GalonesRecibidos = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    CostoXGalon = table.Column<decimal>(type: "money", nullable: false),
                    PrecioTotal = table.Column<decimal>(type: "money", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combustible", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Combustible_Clase",
                        column: x => x.ClaseId,
                        principalTable: "Clase",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Combustible_QuienRecibe_Trabajador",
                        column: x => x.QuienRecibeId,
                        principalTable: "Trabajador",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Combustible_Responsable_Trabajador",
                        column: x => x.ResponsableId,
                        principalTable: "Trabajador",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Combustible_ClaseId",
                table: "Combustible",
                column: "ClaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Combustible_QuienRecibeId",
                table: "Combustible",
                column: "QuienRecibeId");

            migrationBuilder.CreateIndex(
                name: "IX_Combustible_ResponsableId",
                table: "Combustible",
                column: "ResponsableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimiento_Chofer_Trabajador",
                table: "Movimiento",
                column: "ChoferId",
                principalTable: "Trabajador",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimiento_Chofer_Trabajador",
                table: "Movimiento");

            migrationBuilder.DropTable(
                name: "Combustible");

            migrationBuilder.AddColumn<int>(
                name: "TotalMinutos",
                table: "Movimiento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Chofer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApellidoPaterno = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Nombre = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chofer", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Movimiento_Chofer",
                table: "Movimiento",
                column: "ChoferId",
                principalTable: "Chofer",
                principalColumn: "Id");
        }
    }
}
