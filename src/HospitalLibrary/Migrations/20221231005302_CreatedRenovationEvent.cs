using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class CreatedRenovationEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "RenovationRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DomainEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AggregateId = table.Column<int>(type: "int", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RenovationRequestId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DomainEvent_RenovationRequests_RenovationRequestId",
                        column: x => x.RenovationRequestId,
                        principalTable: "RenovationRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppointmentEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentEvent_DomainEvent_Id",
                        column: x => x.Id,
                        principalTable: "DomainEvent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RenovationEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenovationEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RenovationEvent_DomainEvent_Id",
                        column: x => x.Id,
                        principalTable: "DomainEvent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DomainEvent_RenovationRequestId",
                table: "DomainEvent",
                column: "RenovationRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentEvent");

            migrationBuilder.DropTable(
                name: "RenovationEvent");

            migrationBuilder.DropTable(
                name: "DomainEvent");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "RenovationRequests");
        }
    }
}
