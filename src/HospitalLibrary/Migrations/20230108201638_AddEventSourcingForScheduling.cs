using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class AddEventSourcingForScheduling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppointmentSchedulingRootId",
                table: "DomainEvent",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppointmentRoots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Specialization = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentRoots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentScheduledEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentScheduledEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentScheduledEvents_DomainEvent_Id",
                        column: x => x.Id,
                        principalTable: "DomainEvent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppointmentSelectedEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentSelectedEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentSelectedEvents_DomainEvent_Id",
                        column: x => x.Id,
                        principalTable: "DomainEvent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BackClickedEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Step = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackClickedEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BackClickedEvents_DomainEvent_Id",
                        column: x => x.Id,
                        principalTable: "DomainEvent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DateSelectedEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateSelectedEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DateSelectedEvents_DomainEvent_Id",
                        column: x => x.Id,
                        principalTable: "DomainEvent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DoctorSelectedEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSelectedEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorSelectedEvents_DomainEvent_Id",
                        column: x => x.Id,
                        principalTable: "DomainEvent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NextClickedEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Step = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NextClickedEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NextClickedEvents_DomainEvent_Id",
                        column: x => x.Id,
                        principalTable: "DomainEvent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SessionStartedEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionStartedEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionStartedEvents_DomainEvent_Id",
                        column: x => x.Id,
                        principalTable: "DomainEvent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SpecializationSelectedEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Specialization = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecializationSelectedEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecializationSelectedEvents_DomainEvent_Id",
                        column: x => x.Id,
                        principalTable: "DomainEvent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DomainEvent_AppointmentSchedulingRootId",
                table: "DomainEvent",
                column: "AppointmentSchedulingRootId");

            migrationBuilder.AddForeignKey(
                name: "FK_DomainEvent_AppointmentRoots_AppointmentSchedulingRootId",
                table: "DomainEvent",
                column: "AppointmentSchedulingRootId",
                principalTable: "AppointmentRoots",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DomainEvent_AppointmentRoots_AppointmentSchedulingRootId",
                table: "DomainEvent");

            migrationBuilder.DropTable(
                name: "AppointmentRoots");

            migrationBuilder.DropTable(
                name: "AppointmentScheduledEvents");

            migrationBuilder.DropTable(
                name: "AppointmentSelectedEvents");

            migrationBuilder.DropTable(
                name: "BackClickedEvents");

            migrationBuilder.DropTable(
                name: "DateSelectedEvents");

            migrationBuilder.DropTable(
                name: "DoctorSelectedEvents");

            migrationBuilder.DropTable(
                name: "NextClickedEvents");

            migrationBuilder.DropTable(
                name: "SessionStartedEvents");

            migrationBuilder.DropTable(
                name: "SpecializationSelectedEvents");

            migrationBuilder.DropIndex(
                name: "IX_DomainEvent_AppointmentSchedulingRootId",
                table: "DomainEvent");

            migrationBuilder.DropColumn(
                name: "AppointmentSchedulingRootId",
                table: "DomainEvent");
        }
    }
}
