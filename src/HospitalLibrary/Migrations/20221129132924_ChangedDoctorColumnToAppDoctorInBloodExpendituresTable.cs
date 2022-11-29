using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class ChangedDoctorColumnToAppDoctorInBloodExpendituresTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloodExpenditures_Users_DoctorId",
                table: "BloodExpenditures");

            migrationBuilder.AddForeignKey(
                name: "FK_BloodExpenditures_AspNetUsers_DoctorId",
                table: "BloodExpenditures",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloodExpenditures_AspNetUsers_DoctorId",
                table: "BloodExpenditures");

            migrationBuilder.AddForeignKey(
                name: "FK_BloodExpenditures_Users_DoctorId",
                table: "BloodExpenditures",
                column: "DoctorId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
