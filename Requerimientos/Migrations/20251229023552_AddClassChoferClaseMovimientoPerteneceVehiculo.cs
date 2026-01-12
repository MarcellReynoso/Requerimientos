using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Requerimientos.Migrations
{
    /// <inheritdoc />
    public partial class AddClassChoferClaseMovimientoPerteneceVehiculo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chofer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chofer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pertenece",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pertenece", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Placa = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ClaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehiculo_Clase",
                        column: x => x.ClaseId,
                        principalTable: "Clase",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Movimiento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    HoraSalida = table.Column<TimeOnly>(type: "time", nullable: false),
                    HoraIngreso = table.Column<TimeOnly>(type: "time", nullable: false),
                    TotalMinutos = table.Column<int>(type: "int", nullable: false),
                    Motivo = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    HorometroInicio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    HorometroFinal = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    HorometroTotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    VueltasVolq = table.Column<int>(type: "int", nullable: true),
                    TotalM3Desm = table.Column<int>(type: "int", nullable: false),
                    Observaciones = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    HorometroInicio2 = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    HorometroFinal2 = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    HorometroTotal2 = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    VehiculoId = table.Column<int>(type: "int", nullable: false),
                    ChoferId = table.Column<int>(type: "int", nullable: false),
                    PerteneceId = table.Column<int>(type: "int", nullable: false),
                    ProveedorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimiento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimiento_Chofer",
                        column: x => x.ChoferId,
                        principalTable: "Chofer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Movimiento_Pertenece",
                        column: x => x.PerteneceId,
                        principalTable: "Pertenece",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Movimiento_Proveedor",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Movimiento_Vehiculo",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento_ChoferId",
                table: "Movimiento",
                column: "ChoferId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento_PerteneceId",
                table: "Movimiento",
                column: "PerteneceId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento_ProveedorId",
                table: "Movimiento",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento_VehiculoId",
                table: "Movimiento",
                column: "VehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_ClaseId",
                table: "Vehiculo",
                column: "ClaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_Placa",
                table: "Vehiculo",
                column: "Placa",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimiento");

            migrationBuilder.DropTable(
                name: "Chofer");

            migrationBuilder.DropTable(
                name: "Pertenece");

            migrationBuilder.DropTable(
                name: "Vehiculo");

            migrationBuilder.DropTable(
                name: "Clase");
        }
    }
}
