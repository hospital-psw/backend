using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class AddDoctorScheduleAndConsiliumTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorScheduleId",
                table: "VacationRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoctorScheduleId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Consiliums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consiliums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoctorSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorSchedules_AspNetUsers_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConsiliumDoctorSchedule",
                columns: table => new
                {
                    ConsiliumsId = table.Column<int>(type: "int", nullable: false),
                    DoctorsScheduleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsiliumDoctorSchedule", x => new { x.ConsiliumsId, x.DoctorsScheduleId });
                    table.ForeignKey(
                        name: "FK_ConsiliumDoctorSchedule_Consiliums_ConsiliumsId",
                        column: x => x.ConsiliumsId,
                        principalTable: "Consiliums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsiliumDoctorSchedule_DoctorSchedules_DoctorsScheduleId",
                        column: x => x.DoctorsScheduleId,
                        principalTable: "DoctorSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VacationRequests_DoctorScheduleId",
                table: "VacationRequests",
                column: "DoctorScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorScheduleId",
                table: "Appointments",
                column: "DoctorScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsiliumDoctorSchedule_DoctorsScheduleId",
                table: "ConsiliumDoctorSchedule",
                column: "DoctorsScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_DoctorId",
                table: "DoctorSchedules",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_DoctorSchedules_DoctorScheduleId",
                table: "Appointments",
                column: "DoctorScheduleId",
                principalTable: "DoctorSchedules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VacationRequests_DoctorSchedules_DoctorScheduleId",
                table: "VacationRequests",
                column: "DoctorScheduleId",
                principalTable: "DoctorSchedules",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_DoctorSchedules_DoctorScheduleId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_VacationRequests_DoctorSchedules_DoctorScheduleId",
                table: "VacationRequests");

            migrationBuilder.DropTable(
                name: "ConsiliumDoctorSchedule");

            migrationBuilder.DropTable(
                name: "Consiliums");

            migrationBuilder.DropTable(
                name: "DoctorSchedules");

            migrationBuilder.DropIndex(
                name: "IX_VacationRequests_DoctorScheduleId",
                table: "VacationRequests");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DoctorScheduleId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DoctorScheduleId",
                table: "VacationRequests");

            migrationBuilder.DropColumn(
                name: "DoctorScheduleId",
                table: "Appointments");
        }
    }
}
