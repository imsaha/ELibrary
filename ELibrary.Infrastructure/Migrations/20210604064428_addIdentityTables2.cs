using Microsoft.EntityFrameworkCore.Migrations;

namespace ELibrary.Infrastructure.Migrations
{
    public partial class addIdentityTables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "3d04005e-7d68-4e22-aef0-b99749886d59", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "f2317df2-a6a8-4fe2-97a1-d21b0a763b35", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "9e5c1fa2-808a-4dfc-a93a-f98d4a9b1b3d", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "23a9dc98-2da5-4e88-94b3-56ae4d90f8cf", null });
        }
    }
}
