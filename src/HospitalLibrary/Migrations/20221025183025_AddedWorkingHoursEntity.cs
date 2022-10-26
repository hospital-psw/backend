using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class AddedWorkingHoursEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkingHoursId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkingHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHours", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_WorkingHoursId",
                table: "Rooms",
                column: "WorkingHoursId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_WorkingHours_WorkingHoursId",
                table: "Rooms",
                column: "WorkingHoursId",
                principalTable: "WorkingHours",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_WorkingHours_WorkingHoursId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "WorkingHours");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_WorkingHoursId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "WorkingHoursId",
                table: "Rooms");

        }
    }
}