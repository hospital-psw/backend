using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class CreatePatientDiscriminator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddColumn<bool>(
                name: "Guest",
                table: "Users",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guest",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "DateCreated", "DateUpdated", "Deleted", "Floor", "Number" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, "101A" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "DateCreated", "DateUpdated", "Deleted", "Floor", "Number" },
                values: new object[] { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, "204" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "DateCreated", "DateUpdated", "Deleted", "Floor", "Number" },
                values: new object[] { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3, "305B" });
        }
    }
}
