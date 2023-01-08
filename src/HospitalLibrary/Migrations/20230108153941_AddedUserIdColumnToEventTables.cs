using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class AddedUserIdColumnToEventTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "SymptomsChangedEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PrescriptionRemovedEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PrescriptionCreatedEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ExaminationStartedEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ExaminationFinishedEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ExaminationEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "DescriptionCreatedEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SymptomsChangedEvents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PrescriptionRemovedEvents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PrescriptionCreatedEvents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ExaminationStartedEvents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ExaminationFinishedEvents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ExaminationEvents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DescriptionCreatedEvents");
        }
    }
}
