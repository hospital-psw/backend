using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class AddBloodUnitColumnInBloodUnitTherapyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BloodUnitId",
                table: "Therapies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Therapies_BloodUnitId",
                table: "Therapies",
                column: "BloodUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Therapies_BloodUnits_BloodUnitId",
                table: "Therapies",
                column: "BloodUnitId",
                principalTable: "BloodUnits",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Therapies_BloodUnits_BloodUnitId",
                table: "Therapies");

            migrationBuilder.DropIndex(
                name: "IX_Therapies_BloodUnitId",
                table: "Therapies");

            migrationBuilder.DropColumn(
                name: "BloodUnitId",
                table: "Therapies");
        }
    }
}
