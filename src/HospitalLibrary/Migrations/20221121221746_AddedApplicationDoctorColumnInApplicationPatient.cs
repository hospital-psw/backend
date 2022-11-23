using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class AddedApplicationDoctorColumnInApplicationPatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "applicationDoctorId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_applicationDoctorId",
                table: "AspNetUsers",
                column: "applicationDoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_applicationDoctorId",
                table: "AspNetUsers",
                column: "applicationDoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_applicationDoctorId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_applicationDoctorId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "applicationDoctorId",
                table: "AspNetUsers");
        }
    }
}
