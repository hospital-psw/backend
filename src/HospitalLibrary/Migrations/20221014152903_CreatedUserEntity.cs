using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class CreatedUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
            table: "Users",
            columns: new[] { "Id", "Name", "LastName" },
            values: new object[] { 1, "Petar", "Petrovic" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "LastName" },
                values: new object[] { 2, "Marko", "Markovic" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "LastName" },
                values: new object[] { 3, "Djordje", "Djordjevic" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
