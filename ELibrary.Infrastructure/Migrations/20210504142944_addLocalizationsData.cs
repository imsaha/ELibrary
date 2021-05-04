using Microsoft.EntityFrameworkCore.Migrations;

namespace ELibrary.Infrastructure.Migrations
{
    public partial class addLocalizationsData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "Tenents",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "SupportedLanguages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportedLanguages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationCountrySupportedLanguage",
                columns: table => new
                {
                    OperationCountryId = table.Column<long>(type: "bigint", nullable: false),
                    SupportedLanaguageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationCountrySupportedLanguage", x => new { x.OperationCountryId, x.SupportedLanaguageId });
                    table.ForeignKey(
                        name: "FK_OperationCountrySupportedLanguage_OperationCountries_OperationCountryId",
                        column: x => x.OperationCountryId,
                        principalTable: "OperationCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperationCountrySupportedLanguage_SupportedLanguages_SupportedLanaguageId",
                        column: x => x.SupportedLanaguageId,
                        principalTable: "SupportedLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SupportedLanguages",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 1L, "ar-AE", "Arabic" });

            migrationBuilder.InsertData(
                table: "SupportedLanguages",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 2L, "en-US", "English" });

            migrationBuilder.InsertData(
                table: "OperationCountrySupportedLanguage",
                columns: new[] { "OperationCountryId", "SupportedLanaguageId" },
                values: new object[,]
                {
                    { 1L, 1L },
                    { 2L, 1L },
                    { 1L, 2L },
                    { 2L, 2L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tenents_MobileNumber",
                table: "Tenents",
                column: "MobileNumber",
                unique: true,
                filter: "[MobileNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OperationCountrySupportedLanguage_SupportedLanaguageId",
                table: "OperationCountrySupportedLanguage",
                column: "SupportedLanaguageId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportedLanguages_Code",
                table: "SupportedLanguages",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationCountrySupportedLanguage");

            migrationBuilder.DropTable(
                name: "SupportedLanguages");

            migrationBuilder.DropIndex(
                name: "IX_Tenents_MobileNumber",
                table: "Tenents");

            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "Tenents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
