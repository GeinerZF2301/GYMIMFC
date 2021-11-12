using Microsoft.EntityFrameworkCore.Migrations;

namespace GYMIMFC.Migrations
{
    public partial class usuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cita_Empleado_EmpleadoId1",
                table: "Cita");

            migrationBuilder.DropIndex(
                name: "IX_Cita_EmpleadoId1",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "EmpleadoId1",
                table: "Cita");

            migrationBuilder.AlterColumn<string>(
                name: "EmpleadoId",
                table: "Cita",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "ServicioId",
                table: "Cita",
                type: "int",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cita_EmpleadoId",
                table: "Cita",
                column: "EmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cita_Empleado_EmpleadoId",
                table: "Cita",
                column: "EmpleadoId",
                principalTable: "Empleado",
                principalColumn: "EmpleadoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cita_Empleado_EmpleadoId",
                table: "Cita");

            migrationBuilder.DropIndex(
                name: "IX_Cita_EmpleadoId",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "ServicioId",
                table: "Cita");

            migrationBuilder.AlterColumn<int>(
                name: "EmpleadoId",
                table: "Cita",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmpleadoId1",
                table: "Cita",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cita_EmpleadoId1",
                table: "Cita",
                column: "EmpleadoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Cita_Empleado_EmpleadoId1",
                table: "Cita",
                column: "EmpleadoId1",
                principalTable: "Empleado",
                principalColumn: "EmpleadoId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
