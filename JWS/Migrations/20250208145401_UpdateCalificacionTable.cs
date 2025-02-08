using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWS.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCalificacionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NotaTrabajo2",
                table: "Calificaciones",
                newName: "Trabajo");

            migrationBuilder.RenameColumn(
                name: "NotaTrabajo1",
                table: "Calificaciones",
                newName: "Tarea");

            migrationBuilder.RenameColumn(
                name: "NotaTarea",
                table: "Calificaciones",
                newName: "Taller");

            migrationBuilder.RenameColumn(
                name: "NotaExposicion",
                table: "Calificaciones",
                newName: "Quiz2");

            migrationBuilder.RenameColumn(
                name: "NotaExamenFinal",
                table: "Calificaciones",
                newName: "Quiz1");

            migrationBuilder.RenameColumn(
                name: "NotaEvaluacion2",
                table: "Calificaciones",
                newName: "Exposicion");

            migrationBuilder.RenameColumn(
                name: "NotaEvaluacion1",
                table: "Calificaciones",
                newName: "ExamenFinal");

            migrationBuilder.RenameColumn(
                name: "NotaDefinitiva",
                table: "Calificaciones",
                newName: "Definitiva");

            migrationBuilder.RenameColumn(
                name: "NotaActitudinal",
                table: "Calificaciones",
                newName: "Actitudinal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Trabajo",
                table: "Calificaciones",
                newName: "NotaTrabajo2");

            migrationBuilder.RenameColumn(
                name: "Tarea",
                table: "Calificaciones",
                newName: "NotaTrabajo1");

            migrationBuilder.RenameColumn(
                name: "Taller",
                table: "Calificaciones",
                newName: "NotaTarea");

            migrationBuilder.RenameColumn(
                name: "Quiz2",
                table: "Calificaciones",
                newName: "NotaExposicion");

            migrationBuilder.RenameColumn(
                name: "Quiz1",
                table: "Calificaciones",
                newName: "NotaExamenFinal");

            migrationBuilder.RenameColumn(
                name: "Exposicion",
                table: "Calificaciones",
                newName: "NotaEvaluacion2");

            migrationBuilder.RenameColumn(
                name: "ExamenFinal",
                table: "Calificaciones",
                newName: "NotaEvaluacion1");

            migrationBuilder.RenameColumn(
                name: "Definitiva",
                table: "Calificaciones",
                newName: "NotaDefinitiva");

            migrationBuilder.RenameColumn(
                name: "Actitudinal",
                table: "Calificaciones",
                newName: "NotaActitudinal");
        }
    }
}
