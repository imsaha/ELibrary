using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ELibrary.Infrastructure.Migrations
{
    public partial class initDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsProhibited = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationCountries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LateFees = table.Column<double>(type: "float", nullable: false),
                    WeekDays = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationCountries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryBookRentFees",
                columns: table => new
                {
                    OperationCountryId = table.Column<long>(type: "bigint", nullable: false),
                    BookId = table.Column<long>(type: "bigint", nullable: false),
                    RentFee = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryBookRentFees", x => new { x.OperationCountryId, x.BookId });
                    table.ForeignKey(
                        name: "FK_CountryBookRentFees_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryBookRentFees_OperationCountries_OperationCountryId",
                        column: x => x.OperationCountryId,
                        principalTable: "OperationCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OperationCountryHoliday",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationCountryId = table.Column<long>(type: "bigint", nullable: false),
                    OccationTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HolidayDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationCountryHoliday", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationCountryHoliday_OperationCountries_OperationCountryId",
                        column: x => x.OperationCountryId,
                        principalTable: "OperationCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    OperationCountryId = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduledReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rents_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rents_OperationCountries_OperationCountryId",
                        column: x => x.OperationCountryId,
                        principalTable: "OperationCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rents_Tenents_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Description", "IsProhibited", "Title" },
                values: new object[,]
                {
                    { 1L, "Our earliest experiences shape our lives far down the road, and What Happened to You? provides powerful scientific and emotional insights into the behavioral patterns so many of us struggle to understand.", false, "What Happened to You?: Conversations on Trauma, Resilience, and Healing" },
                    { 2L, "People unfamiliar with Scripture often assume that women play a small, secondary role in the Bible. But in fact, they were central figures in numerous Biblical tales. It was Queen Esther’s bravery at a vital point in history which saved her entire people. The Bible contains warriors like Jael, judges like Deborah, and prophets like Miriam. The first person to witness Jesus’ resurrection was Mary Magdalene, who promptly became the first Christian evangelist, eager to share the news which would change the world forever.", false, "The Women of the Bible Speak: The Wisdom of 16 Women and Their Lessons for Today" },
                    { 3L, "In The Four Agreements, bestselling author don Miguel Ruiz reveals the source of self-limiting beliefs that rob us of joy and create needless suffering. Based on ancient Toltec wisdom, The Four Agreements offer a powerful code of conduct that can rapidly transform our lives to a new experience of freedom, true happiness, and love.", false, "The Four Agreements: A Practical Guide to Personal Freedom (A Toltec Wisdom Book)" },
                    { 4L, "Somewhere out beyond the edge of the universe there is a library that contains an infinite number of books, each one the story of another reality. One tells the story of your life as it is, along with another book for the other life you could have lived if you had made a different choice at any point in your life. While we all wonder how our lives might have been, what if you had the chance to go to the library and see for yourself? Would any of these other lives truly be better?", false, "The Midnight Library: A Novel" }
                });

            migrationBuilder.InsertData(
                table: "OperationCountries",
                columns: new[] { "Id", "Alias", "Currency", "LateFees", "Name", "WeekDays" },
                values: new object[,]
                {
                    { 1L, "UAE", "AED", 5.0, "United Arab Emirates", "Friday,Saturday" },
                    { 2L, "IN", "INR", 100.0, "India", "Sunday" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountryBookRentFees_BookId",
                table: "CountryBookRentFees",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationCountryHoliday_OperationCountryId",
                table: "OperationCountryHoliday",
                column: "OperationCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_BookId",
                table: "Rents",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_OperationCountryId",
                table: "Rents",
                column: "OperationCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_TenantId",
                table: "Rents",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountryBookRentFees");

            migrationBuilder.DropTable(
                name: "OperationCountryHoliday");

            migrationBuilder.DropTable(
                name: "Rents");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "OperationCountries");

            migrationBuilder.DropTable(
                name: "Tenents");
        }
    }
}
