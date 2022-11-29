using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class ChangedDoctorColumnToAppDoctorInBloodAcquisitions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloodAcquisitions_Users_DoctorId",
                table: "BloodAcquisitions");

            migrationBuilder.AddForeignKey(
                name: "FK_BloodAcquisitions_AspNetUsers_DoctorId",
                table: "BloodAcquisitions",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloodAcquisitions_AspNetUsers_DoctorId",
                table: "BloodAcquisitions");

            migrationBuilder.AddForeignKey(
                name: "FK_BloodAcquisitions_Users_DoctorId",
                table: "BloodAcquisitions",
                column: "DoctorId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
