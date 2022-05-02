using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IncidentService.Migrations
{
    public partial class NewMigartion : Migration
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
                values: new object[] { new Guid("fcd2fe08-7910-4a65-a8bd-72d447704d79"), "testName" });

            migrationBuilder.InsertData(
                table: "Incidents",
                columns: new[] { "IncidentId", "ActionDescription", "CategoryId", "Date", "Description", "Designation", "FurtherAction", "FurtherActionPerson", "ProblemSolved", "Remarks", "ReportedBy", "Significance", "SolvingDate", "ThirdPartyHelp", "Time", "UserId", "Verifies", "Workspace" },
                values: new object[] { new Guid("98d1749a-af21-4883-a272-0906ee901fcf"), "test", new Guid("fcd2fe08-7910-4a65-a8bd-72d447704d79"), new DateTime(2022, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "test01", true, "test", "test", "test", "test", 1, new DateTime(2022, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2022, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("47c7aa3d-8e7e-4290-8158-7dfcf00ff376"), "test", "test" });
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
