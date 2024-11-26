using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWS.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asistencias_Ciclos_CicloId",
                table: "Asistencias");

            migrationBuilder.DropForeignKey(
                name: "FK_CicloMaterias_Ciclos_CicloId",
                table: "CicloMaterias");

            migrationBuilder.DropColumn(
                name: "Ausente",
                table: "Asistencias");

            migrationBuilder.AddForeignKey(
                name: "FK_Asistencias_Ciclos_CicloId",
                table: "Asistencias",
                column: "CicloId",
                principalTable: "Ciclos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CicloMaterias_Ciclos_CicloId",
                table: "CicloMaterias",
                column: "CicloId",
                principalTable: "Ciclos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asistencias_Ciclos_CicloId",
                table: "Asistencias");

            migrationBuilder.DropForeignKey(
                name: "FK_CicloMaterias_Ciclos_CicloId",
                table: "CicloMaterias");

            migrationBuilder.AddColumn<bool>(
                name: "Ausente",
                table: "Asistencias",
                type: "bit",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Asistencias_Ciclos_CicloId",
                table: "Asistencias",
                column: "CicloId",
                principalTable: "Ciclos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CicloMaterias_Ciclos_CicloId",
                table: "CicloMaterias",
                column: "CicloId",
                principalTable: "Ciclos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
