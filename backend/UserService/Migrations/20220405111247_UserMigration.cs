using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserService.Migrations
{
    public partial class UserMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "register",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("66c72bcd-43ac-4914-a2e9-856fac56b891"))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_register", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_register_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "RoleId", "UserType" },
                values: new object[] { new Guid("66c72bcd-43ac-4914-a2e9-856fac56b891"), "User" });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "RoleId", "UserType" },
                values: new object[] { new Guid("e4711e98-5cb3-4ebc-8c2e-1f90c54f9255"), "Admin" });

            migrationBuilder.InsertData(
                table: "register",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "Password", "RoleId" },
                values: new object[] { new Guid("f0077164-4446-45d6-9185-aa2d60aee928"), "marko@gmail.com", "Marko", "Milic", "123456", new Guid("66c72bcd-43ac-4914-a2e9-856fac56b891") });

            migrationBuilder.InsertData(
                table: "register",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "Password", "RoleId" },
                values: new object[] { new Guid("089c377b-6464-4692-987e-483d556753dc"), "Nikola@gmail.com", "Nikola", "Milic", "123456", new Guid("e4711e98-5cb3-4ebc-8c2e-1f90c54f9255") });

            migrationBuilder.CreateIndex(
                name: "IX_register_RoleId",
                table: "register",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "register");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
