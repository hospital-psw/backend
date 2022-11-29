using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class ChangedDoctorColumnToAppDoctorInVacationRequestsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacationRequests_Users_DoctorId",
                table: "VacationRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_VacationRequests_AspNetUsers_DoctorId",
                table: "VacationRequests",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacationRequests_AspNetUsers_DoctorId",
                table: "VacationRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_VacationRequests_Users_DoctorId",
                table: "VacationRequests",
                column: "DoctorId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
