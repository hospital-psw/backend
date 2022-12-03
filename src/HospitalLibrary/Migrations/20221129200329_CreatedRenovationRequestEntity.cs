using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class CreatedRenovationRequestEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RenovationRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RenovationType = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenovationRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RenovationDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewRoomName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewRoomPurpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RenovationRequestId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenovationDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RenovationDetails_RenovationRequests_RenovationRequestId",
                        column: x => x.RenovationRequestId,
                        principalTable: "RenovationRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RenovationRequestRoom",
                columns: table => new
                {
                    RenovationsId = table.Column<int>(type: "int", nullable: false),
                    RoomsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenovationRequestRoom", x => new { x.RenovationsId, x.RoomsId });
                    table.ForeignKey(
                        name: "FK_RenovationRequestRoom_RenovationRequests_RenovationsId",
                        column: x => x.RenovationsId,
                        principalTable: "RenovationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RenovationRequestRoom_Rooms_RoomsId",
                        column: x => x.RoomsId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RenovationDetails_RenovationRequestId",
                table: "RenovationDetails",
                column: "RenovationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RenovationRequestRoom_RoomsId",
                table: "RenovationRequestRoom",
                column: "RoomsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RenovationDetails");

            migrationBuilder.DropTable(
                name: "RenovationRequestRoom");

            migrationBuilder.DropTable(
                name: "RenovationRequests");
        }
    }
}
