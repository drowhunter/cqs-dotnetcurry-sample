using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cqs.CoreSampleLib.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Authors = table.Column<string>(nullable: true),
                    DatePublished = table.Column<DateTime>(nullable: false),
                    InMyPossession = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Authors", "DatePublished", "InMyPossession", "Title" },
                values: new object[] { 1, "J.R.Tolkien", new DateTime(2010, 12, 21, 18, 16, 7, 581, DateTimeKind.Utc).AddTicks(8960), true, "Lord of the Rings" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Authors", "DatePublished", "InMyPossession", "Title" },
                values: new object[] { 2, "Blake Harris", new DateTime(2010, 12, 21, 18, 16, 7, 582, DateTimeKind.Utc).AddTicks(1299), true, "The History of the Future" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
