using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IncidentService.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Incidents",
                columns: table => new
                {
                    IncidentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidents", x => x.IncidentId);
                    table.ForeignKey(
                        name: "FK_Incidents_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[] { new Guid("33fd5821-8a8b-421a-b529-1fcfe57195cb"), "testName" });

            migrationBuilder.InsertData(
                table: "Incidents",
                columns: new[] { "IncidentId", "ActionDescription", "CategoryId", "Date", "Description", "FurtherAction", "FurtherActionPerson", "Number", "ProblemSolved", "Remarks", "Significance", "SolvingDate", "ThirdPartyHelp", "Time", "UserId", "Verifies", "Workspace" },
                values: new object[] { new Guid("64724124-721c-449a-b9e6-cc2217b36052"), "test", new Guid("33fd5821-8a8b-421a-b529-1fcfe57195cb"), new DateTime(2022, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", true, "test", "test01", "test", "test", 1, new DateTime(2022, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2022, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("95342baa-73c9-4202-b558-ded5ffe060f7"), "test", "test" });

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_CategoryId",
                table: "Incidents",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Incidents");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
