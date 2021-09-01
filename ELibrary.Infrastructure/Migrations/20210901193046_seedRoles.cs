using Microsoft.EntityFrameworkCore.Migrations;

namespace ELibrary.Infrastructure.Migrations
{
    public partial class seedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityRole<long>");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "tblRoles",
                newName: "NormalizedId");

            migrationBuilder.InsertData(
                table: "tblRoles",
                columns: new[] { "Id", "NormalizedId" },
                values: new object[] { "user", "USERS" });

            migrationBuilder.InsertData(
                table: "tblRoles",
                columns: new[] { "Id", "NormalizedId" },
                values: new object[] { "admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tblRoles",
                keyColumn: "Id",
                keyValue: "admin");

            migrationBuilder.DeleteData(
                table: "tblRoles",
                keyColumn: "Id",
                keyValue: "user");

            migrationBuilder.RenameColumn(
                name: "NormalizedId",
                table: "tblRoles",
                newName: "Description");

            migrationBuilder.CreateTable(
                name: "IdentityRole<long>",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole<long>", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole<long>",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 1L, "c4225312-e81b-42d7-b2c6-dad598cee700", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "IdentityRole<long>",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 2L, "56a18cfa-4e78-4d20-97a4-0634f137af8e", "user", "USER" });
        }
    }
}
