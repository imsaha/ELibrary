using Microsoft.EntityFrameworkCore.Migrations;

namespace ELibrary.Infrastructure.Migrations
{
    public partial class renameTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CountryBookRentFees_Books_BookId",
                table: "CountryBookRentFees");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryBookRentFees_OperationCountries_OperationCountryId",
                table: "CountryBookRentFees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryBookRentFees",
                table: "CountryBookRentFees");

            migrationBuilder.RenameTable(
                name: "CountryBookRentFees",
                newName: "CountryWiseBookRentFees");

            migrationBuilder.RenameIndex(
                name: "IX_CountryBookRentFees_BookId",
                table: "CountryWiseBookRentFees",
                newName: "IX_CountryWiseBookRentFees_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryWiseBookRentFees",
                table: "CountryWiseBookRentFees",
                columns: new[] { "OperationCountryId", "BookId" });

            migrationBuilder.InsertData(
                table: "CountryWiseBookRentFees",
                columns: new[] { "BookId", "OperationCountryId", "RentFee" },
                values: new object[,]
                {
                    { 1L, 1L, 10.0 },
                    { 1L, 2L, 200.0 },
                    { 2L, 1L, 13.0 },
                    { 2L, 2L, 260.0 },
                    { 3L, 1L, 11.0 },
                    { 3L, 2L, 240.0 },
                    { 4L, 1L, 11.0 },
                    { 4L, 2L, 240.0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CountryWiseBookRentFees_Books_BookId",
                table: "CountryWiseBookRentFees",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryWiseBookRentFees_OperationCountries_OperationCountryId",
                table: "CountryWiseBookRentFees",
                column: "OperationCountryId",
                principalTable: "OperationCountries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CountryWiseBookRentFees_Books_BookId",
                table: "CountryWiseBookRentFees");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryWiseBookRentFees_OperationCountries_OperationCountryId",
                table: "CountryWiseBookRentFees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryWiseBookRentFees",
                table: "CountryWiseBookRentFees");

            migrationBuilder.DeleteData(
                table: "CountryWiseBookRentFees",
                keyColumns: new[] { "BookId", "OperationCountryId" },
                keyValues: new object[] { 1L, 1L });

            migrationBuilder.DeleteData(
                table: "CountryWiseBookRentFees",
                keyColumns: new[] { "BookId", "OperationCountryId" },
                keyValues: new object[] { 2L, 1L });

            migrationBuilder.DeleteData(
                table: "CountryWiseBookRentFees",
                keyColumns: new[] { "BookId", "OperationCountryId" },
                keyValues: new object[] { 3L, 1L });

            migrationBuilder.DeleteData(
                table: "CountryWiseBookRentFees",
                keyColumns: new[] { "BookId", "OperationCountryId" },
                keyValues: new object[] { 4L, 1L });

            migrationBuilder.DeleteData(
                table: "CountryWiseBookRentFees",
                keyColumns: new[] { "BookId", "OperationCountryId" },
                keyValues: new object[] { 1L, 2L });

            migrationBuilder.DeleteData(
                table: "CountryWiseBookRentFees",
                keyColumns: new[] { "BookId", "OperationCountryId" },
                keyValues: new object[] { 2L, 2L });

            migrationBuilder.DeleteData(
                table: "CountryWiseBookRentFees",
                keyColumns: new[] { "BookId", "OperationCountryId" },
                keyValues: new object[] { 3L, 2L });

            migrationBuilder.DeleteData(
                table: "CountryWiseBookRentFees",
                keyColumns: new[] { "BookId", "OperationCountryId" },
                keyValues: new object[] { 4L, 2L });

            migrationBuilder.RenameTable(
                name: "CountryWiseBookRentFees",
                newName: "CountryBookRentFees");

            migrationBuilder.RenameIndex(
                name: "IX_CountryWiseBookRentFees_BookId",
                table: "CountryBookRentFees",
                newName: "IX_CountryBookRentFees_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryBookRentFees",
                table: "CountryBookRentFees",
                columns: new[] { "OperationCountryId", "BookId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CountryBookRentFees_Books_BookId",
                table: "CountryBookRentFees",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryBookRentFees_OperationCountries_OperationCountryId",
                table: "CountryBookRentFees",
                column: "OperationCountryId",
                principalTable: "OperationCountries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
