using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class CreatedRelocationRequestEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "RelocationRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromRoomId = table.Column<int>(type: "int", nullable: true),
                    ToRoomId = table.Column<int>(type: "int", nullable: true),
                    EquipmentId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelocationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelocationRequests_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RelocationRequests_Rooms_FromRoomId",
                        column: x => x.FromRoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RelocationRequests_Rooms_ToRoomId",
                        column: x => x.ToRoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RelocationRequests_EquipmentId",
                table: "RelocationRequests",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_RelocationRequests_FromRoomId",
                table: "RelocationRequests",
                column: "FromRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RelocationRequests_ToRoomId",
                table: "RelocationRequests",
                column: "ToRoomId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelocationRequests");
        }
    }
}
