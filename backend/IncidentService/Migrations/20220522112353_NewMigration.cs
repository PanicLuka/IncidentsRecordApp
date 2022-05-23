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
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Significance = table.Column<int>(type: "int", nullable: false),
                    Workspace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThirdPartyHelp = table.Column<bool>(type: "bit", nullable: false),
                    ProblemSolved = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FurtherAction = table.Column<bool>(type: "bit", nullable: false),
                    FurtherActionPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SolvingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Verifies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidents", x => x.IncidentId);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[] { new Guid("c582a6b6-afb3-4a33-9d3d-870037bd629f"), "testName" });

            migrationBuilder.InsertData(
                table: "Incidents",
                columns: new[] { "IncidentId", "ActionDescription", "CategoryId", "Date", "Description", "Designation", "FurtherAction", "FurtherActionPerson", "ProblemSolved", "Remarks", "ReportedBy", "Significance", "SolvingDate", "ThirdPartyHelp", "Time", "UserId", "Verifies", "Workspace" },
                values: new object[] { new Guid("8b1003e0-79fa-44ba-8f58-665f490477f1"), "test", new Guid("c582a6b6-afb3-4a33-9d3d-870037bd629f"), new DateTime(2022, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "test01", true, "test", "test", "test", "test", 1, new DateTime(2022, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "00:00", new Guid("bf59a03d-da1e-4613-8df2-203b013fde09"), "test", "test" });
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
