using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class RelocationQuantityVO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "RelocationRequests",
                newName: "Quantity_Quantity");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity_Quantity",
                table: "RelocationRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity_Quantity",
                table: "RelocationRequests",
                newName: "Quantity");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
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
