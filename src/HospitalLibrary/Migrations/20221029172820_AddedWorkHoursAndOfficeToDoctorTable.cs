using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class AddedWorkHoursAndOfficeToDoctorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkHoursId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_OfficeId",
                table: "Users",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_WorkHoursId",
                table: "Users",
                column: "WorkHoursId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Rooms_OfficeId",
                table: "Users",
                column: "OfficeId",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_WorkingHours_WorkHoursId",
                table: "Users",
                column: "WorkHoursId",
                principalTable: "WorkingHours",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Rooms_OfficeId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_WorkingHours_WorkHoursId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_OfficeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_WorkHoursId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WorkHoursId",
                table: "Users");
        }
    }
}
