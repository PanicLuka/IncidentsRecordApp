using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserService.Migrations
{
    public partial class UserMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessPermission = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("16aee3f2-6ee5-4d2b-8ddd-22670af9f38f"))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => new { x.UserId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_UserPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "AccessPermission" },
                values: new object[,]
                {
                    { new Guid("114ea29f-07f2-4448-a9d5-9a13c18f43f6"), "UserGetAll" },
                    { new Guid("f268f7b3-a362-4945-84d8-88d7ddce5bdc"), "UserDelete" },
                    { new Guid("810c68f3-4405-4f2c-aa5c-0310aa344f7e"), "UserUpdate" },
                    { new Guid("fb5ad568-2823-4ff7-83ad-4fd6170ac616"), "UserGetById" },
                    { new Guid("47dfae26-ab31-4bd8-8eaf-e697b2007f6e"), "UserCreateUser" },
                    { new Guid("b5f48801-8140-41df-941e-586cc221e477"), "IncidentsGetAll" },
                    { new Guid("61a94456-07f6-407c-b1ff-0d3032b7482e"), "IncidentsGetById" },
                    { new Guid("3a625004-b54f-47ff-ae20-2363f486f353"), "IncidentsUpdate" },
                    { new Guid("97fc05bf-4c22-4e5d-8fa2-91a478c1cde1"), "IncidentsDelete" },
                    { new Guid("d04348cc-d878-45e0-b7b2-35b35233a99d"), "IncidentsCreate" },
                    { new Guid("0e40bba4-6085-4cdc-af56-015402e533ca"), "PromoteToAdmin" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "UserType" },
                values: new object[,]
                {
                    { new Guid("16aee3f2-6ee5-4d2b-8ddd-22670af9f38f"), "User" },
                    { new Guid("501fbb12-98ed-4bfa-af87-e916eab08d11"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("b5f48801-8140-41df-941e-586cc221e477"), new Guid("16aee3f2-6ee5-4d2b-8ddd-22670af9f38f") },
                    { new Guid("b5f48801-8140-41df-941e-586cc221e477"), new Guid("501fbb12-98ed-4bfa-af87-e916eab08d11") },
                    { new Guid("61a94456-07f6-407c-b1ff-0d3032b7482e"), new Guid("501fbb12-98ed-4bfa-af87-e916eab08d11") },
                    { new Guid("3a625004-b54f-47ff-ae20-2363f486f353"), new Guid("501fbb12-98ed-4bfa-af87-e916eab08d11") },
                    { new Guid("97fc05bf-4c22-4e5d-8fa2-91a478c1cde1"), new Guid("501fbb12-98ed-4bfa-af87-e916eab08d11") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "Password", "RoleId" },
                values: new object[,]
                {
                    { new Guid("ef72df04-f4b6-4d20-8fdd-d02ab10a083d"), "marko@gmail.com", "Marko", "Milic", "123456", new Guid("16aee3f2-6ee5-4d2b-8ddd-22670af9f38f") },
                    { new Guid("cdc5f80a-97c6-4c1d-a5d4-9cd39e196309"), "Nikola@gmail.com", "Nikola", "Milic", "123456", new Guid("501fbb12-98ed-4bfa-af87-e916eab08d11") }
                });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "PermissionId", "UserId" },
                values: new object[] { new Guid("97fc05bf-4c22-4e5d-8fa2-91a478c1cde1"), new Guid("ef72df04-f4b6-4d20-8fdd-d02ab10a083d") });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "PermissionId", "UserId" },
                values: new object[] { new Guid("d04348cc-d878-45e0-b7b2-35b35233a99d"), new Guid("cdc5f80a-97c6-4c1d-a5d4-9cd39e196309") });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_PermissionId",
                table: "UserPermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
