using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWS.Migrations
{
    /// <inheritdoc />
    public partial class calificacionesmodified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Calificaciones_EstudianteId",
                table: "Calificaciones");

            migrationBuilder.AddColumn<decimal>(
                name: "NotaExposicion",
                table: "Calificaciones",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "NotaTarea",
                table: "Calificaciones",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_EstudianteId",
                table: "Calificaciones",
                column: "EstudianteId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Calificaciones_EstudianteId",
                table: "Calificaciones");

            migrationBuilder.DropColumn(
                name: "NotaExposicion",
                table: "Calificaciones");

            migrationBuilder.DropColumn(
                name: "NotaTarea",
                table: "Calificaciones");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_EstudianteId",
                table: "Calificaciones",
                column: "EstudianteId");
        }
    }
}
