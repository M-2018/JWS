using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWS.Migrations
{
    /// <inheritdoc />
    public partial class Studentcorrected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoUnico",
                table: "Estudiantes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoUnico",
                table: "Estudiantes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
