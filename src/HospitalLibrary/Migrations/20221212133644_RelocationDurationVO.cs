using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class RelocationDurationVO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "RelocationRequests",
                newName: "Duration_Duration");

            migrationBuilder.AlterColumn<int>(
                name: "Duration_Duration",
                table: "RelocationRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.RenameColumn(
                name: "Duration_Duration",
                table: "RelocationRequests",
                newName: "Duration");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "RelocationRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
