using Microsoft.EntityFrameworkCore.Migrations;

namespace ELibrary.Infrastructure.Migrations
{
    public partial class seedRoles2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "tblRoles",
                keyColumn: "Id",
                keyValue: "user",
                column: "NormalizedId",
                value: "USER");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "tblRoles",
                keyColumn: "Id",
                keyValue: "user",
                column: "NormalizedId",
                value: "USERS");
        }
    }
}
