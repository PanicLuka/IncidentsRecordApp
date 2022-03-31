using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IncidentService.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_CategoryId1",
                        column: x => x.CategoryId1,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Incidents",
                columns: table => new
                {
                    IncidentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Significance = table.Column<int>(type: "int", nullable: false),
                    Workspace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThirdPartyHelp = table.Column<bool>(type: "bit", nullable: false),
                    ProblemSolved = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FurtherAction = table.Column<bool>(type: "bit", nullable: false),
                    FurtherActionPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SolvingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Verifies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidents", x => x.IncidentId);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryId1", "CategoryName" },
                values: new object[] { 1, null, "testName" });

            migrationBuilder.InsertData(
                table: "Incidents",
                columns: new[] { "IncidentId", "ActionDescription", "CategoryId", "Date", "Description", "FurtherAction", "FurtherActionPerson", "Number", "ProblemSolved", "Remarks", "Significance", "SolvingDate", "ThirdPartyHelp", "Time", "UserId", "Verifies", "Workspace" },
                values: new object[] { 1, "test", 1, new DateTime(2022, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", true, "test", "test01", "test", "test", 1, new DateTime(2022, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2022, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "test", "test" });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryId1",
                table: "Categories",
                column: "CategoryId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Incidents");
        }
    }
}
