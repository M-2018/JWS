using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWS.Migrations
{
    /// <inheritdoc />
    public partial class Corregidaciclosmaterias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materias_Ciclos_CicloId",
                table: "Materias");

            migrationBuilder.DropIndex(
                name: "IX_Materias_CicloId",
                table: "Materias");

            migrationBuilder.DropColumn(
                name: "CicloId",
                table: "Materias");

            migrationBuilder.CreateTable(
                name: "CicloMaterias",
                columns: table => new
                {
                    CicloId = table.Column<long>(type: "bigint", nullable: false),
                    MateriaId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CicloMaterias", x => new { x.CicloId, x.MateriaId });
                    table.ForeignKey(
                        name: "FK_CicloMaterias_Ciclos_CicloId",
                        column: x => x.CicloId,
                        principalTable: "Ciclos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CicloMaterias_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CicloMaterias_MateriaId",
                table: "CicloMaterias",
                column: "MateriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CicloMaterias");

            migrationBuilder.AddColumn<long>(
                name: "CicloId",
                table: "Materias",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Materias_CicloId",
                table: "Materias",
                column: "CicloId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materias_Ciclos_CicloId",
                table: "Materias",
                column: "CicloId",
                principalTable: "Ciclos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
