using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class AddedRoomToConsilium : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Consiliums",
                type: "int",
                nullable: true);


            migrationBuilder.CreateIndex(
                name: "IX_Consiliums_RoomId",
                table: "Consiliums",
                column: "RoomId");


            migrationBuilder.AddForeignKey(
                name: "FK_Consiliums_Rooms_RoomId",
                table: "Consiliums",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consiliums_Rooms_RoomId",
                table: "Consiliums");

            migrationBuilder.DropTable(
                name: "RenovationDetails");

            migrationBuilder.DropTable(
                name: "RenovationRequestRoom");

            migrationBuilder.DropTable(
                name: "RenovationRequests");

            migrationBuilder.DropIndex(
                name: "IX_Consiliums_RoomId",
                table: "Consiliums");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Consiliums");
        }
    }
}
