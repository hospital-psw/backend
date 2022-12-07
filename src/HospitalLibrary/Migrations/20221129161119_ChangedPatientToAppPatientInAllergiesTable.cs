using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class ChangedPatientToAppPatientInAllergiesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allergies_AspNetUsers_ApplicationPatientId",
                table: "Allergies");

            migrationBuilder.DropTable(
                name: "AllergiesPatient");

            migrationBuilder.DropIndex(
                name: "IX_Allergies_ApplicationPatientId",
                table: "Allergies");

            migrationBuilder.DropColumn(
                name: "ApplicationPatientId",
                table: "Allergies");

            migrationBuilder.CreateTable(
                name: "AllergiesApplicationPatient",
                columns: table => new
                {
                    AllergiesId = table.Column<int>(type: "int", nullable: false),
                    PatientsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllergiesApplicationPatient", x => new { x.AllergiesId, x.PatientsId });
                    table.ForeignKey(
                        name: "FK_AllergiesApplicationPatient_Allergies_AllergiesId",
                        column: x => x.AllergiesId,
                        principalTable: "Allergies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllergiesApplicationPatient_AspNetUsers_PatientsId",
                        column: x => x.PatientsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllergiesApplicationPatient_PatientsId",
                table: "AllergiesApplicationPatient",
                column: "PatientsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllergiesApplicationPatient");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationPatientId",
                table: "Allergies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AllergiesPatient",
                columns: table => new
                {
                    AllergiesId = table.Column<int>(type: "int", nullable: false),
                    PatientsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllergiesPatient", x => new { x.AllergiesId, x.PatientsId });
                    table.ForeignKey(
                        name: "FK_AllergiesPatient_Allergies_AllergiesId",
                        column: x => x.AllergiesId,
                        principalTable: "Allergies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllergiesPatient_Users_PatientsId",
                        column: x => x.PatientsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Allergies_ApplicationPatientId",
                table: "Allergies",
                column: "ApplicationPatientId");

            migrationBuilder.CreateIndex(
                name: "IX_AllergiesPatient_PatientsId",
                table: "AllergiesPatient",
                column: "PatientsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Allergies_AspNetUsers_ApplicationPatientId",
                table: "Allergies",
                column: "ApplicationPatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
