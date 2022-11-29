using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class ChangedPatientToAppPatientInFeedbackTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Users_CreatorId",
                table: "Feedback");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_AspNetUsers_CreatorId",
                table: "Feedback",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddForeignKey(
               name: "FK_Feedback_Users_CreatorId",
               table: "Feedback",
               column: "CreatorId",
               principalTable: "Users",
               principalColumn: "Id");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_AspNetUsers_CreatorId",
                table: "Feedback");
        }
    }
}
