using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GYMIMFC.Data.Migrations
{
    public partial class migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    idCategoria = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.idCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    idCliente = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Peso = table.Column<double>(type: "float", nullable: false),
                    IMC = table.Column<double>(type: "float", nullable: false),
                    Altura = table.Column<double>(type: "float", nullable: false),
                    Edad = table.Column<short>(type: "smallint", nullable: false),
                    FechaPagoMatricula = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlanEntrenamiento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.idCliente);
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    idEmpleado = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Puesto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZonaPuesto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TiempoContrato = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.idEmpleado);
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    idServicio = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroMatricula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idCategoria = table.Column<short>(type: "smallint", nullable: false),
                    CategoriaidCategoria = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.idServicio);
                    table.ForeignKey(
                        name: "FK_Servicios_Categoria_CategoriaidCategoria",
                        column: x => x.CategoriaidCategoria,
                        principalTable: "Categoria",
                        principalColumn: "idCategoria",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Matricula",
                columns: table => new
                {
                    Horario = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idCliente = table.Column<short>(type: "smallint", nullable: false),
                    idEmpleado = table.Column<short>(type: "smallint", nullable: false),
                    idServicio = table.Column<short>(type: "smallint", nullable: false),
                    ClienteidCliente = table.Column<short>(type: "smallint", nullable: true),
                    EmpleadoidEmpleado = table.Column<short>(type: "smallint", nullable: true),
                    ServiciosidServicio = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matricula", x => x.Horario);
                    table.ForeignKey(
                        name: "FK_Matricula_Cliente_ClienteidCliente",
                        column: x => x.ClienteidCliente,
                        principalTable: "Cliente",
                        principalColumn: "idCliente",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matricula_Empleado_EmpleadoidEmpleado",
                        column: x => x.EmpleadoidEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "idEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matricula_Servicios_ServiciosidServicio",
                        column: x => x.ServiciosidServicio,
                        principalTable: "Servicios",
                        principalColumn: "idServicio",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matricula_ClienteidCliente",
                table: "Matricula",
                column: "ClienteidCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Matricula_EmpleadoidEmpleado",
                table: "Matricula",
                column: "EmpleadoidEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Matricula_ServiciosidServicio",
                table: "Matricula",
                column: "ServiciosidServicio");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_CategoriaidCategoria",
                table: "Servicios",
                column: "CategoriaidCategoria");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matricula");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
