using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class CreateMedicamentAllergensTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllergiesMedicament",
                columns: table => new
                {
                    AllergensId = table.Column<int>(type: "int", nullable: false),
                    MedicamentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllergiesMedicament", x => new { x.AllergensId, x.MedicamentsId });
                    table.ForeignKey(
                        name: "FK_AllergiesMedicament_Allergies_AllergensId",
                        column: x => x.AllergensId,
                        principalTable: "Allergies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllergiesMedicament_Medicaments_MedicamentsId",
                        column: x => x.MedicamentsId,
                        principalTable: "Medicaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllergiesMedicament_MedicamentsId",
                table: "AllergiesMedicament",
                column: "MedicamentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllergiesMedicament");
        }
    }
}
