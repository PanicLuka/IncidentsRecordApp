using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserService.Migrations
{
    public partial class NewMigration : Migration
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
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("d8a15fcf-d43e-45cc-a249-9eb0ea1761bc"))
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
                    { new Guid("552c0e38-a382-4bcd-ab83-0dc7ff887bcc"), "UserGetAll" },
                    { new Guid("bee40091-1342-4f28-ab44-fd95fe7f71e3"), "UserDelete" },
                    { new Guid("83774e28-705a-431e-a02b-2f94ce8263f8"), "UserUpdate" },
                    { new Guid("f56e749a-dfbb-4ff9-b9dd-3f3ea8007138"), "UserGetById" },
                    { new Guid("309f3acc-efb7-4330-a6b3-e7b90bb0c690"), "UserCreateUser" },
                    { new Guid("eb3e606b-346d-4390-9343-00fa7c1c2133"), "IncidentsGetAll" },
                    { new Guid("ae00b7e7-2f17-4cf3-a328-68b67f34fbaf"), "IncidentsGetById" },
                    { new Guid("ae44e101-e29b-48c0-9a17-9091e1325b13"), "IncidentsUpdate" },
                    { new Guid("1f5619cd-a38c-4eb8-aa13-8488b9995288"), "IncidentsDelete" },
                    { new Guid("c4433744-1ffe-49d6-8de7-fd1783737e5d"), "IncidentsCreate" },
                    { new Guid("45f50ce6-576b-42a2-8507-1370eeb7ad3a"), "PromoteToAdmin" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "UserType" },
                values: new object[,]
                {
                    { new Guid("d8a15fcf-d43e-45cc-a249-9eb0ea1761bc"), "User" },
                    { new Guid("4f0849fd-5033-4d51-8e07-d5c5db42431c"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("eb3e606b-346d-4390-9343-00fa7c1c2133"), new Guid("d8a15fcf-d43e-45cc-a249-9eb0ea1761bc") },
                    { new Guid("eb3e606b-346d-4390-9343-00fa7c1c2133"), new Guid("4f0849fd-5033-4d51-8e07-d5c5db42431c") },
                    { new Guid("ae00b7e7-2f17-4cf3-a328-68b67f34fbaf"), new Guid("4f0849fd-5033-4d51-8e07-d5c5db42431c") },
                    { new Guid("ae44e101-e29b-48c0-9a17-9091e1325b13"), new Guid("4f0849fd-5033-4d51-8e07-d5c5db42431c") },
                    { new Guid("1f5619cd-a38c-4eb8-aa13-8488b9995288"), new Guid("4f0849fd-5033-4d51-8e07-d5c5db42431c") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "Password", "RoleId" },
                values: new object[,]
                {
                    { new Guid("ae2799ec-722d-41fb-bba1-748f9e8a782c"), "marko@gmail.com", "Marko", "Milic", "$2a$11$8Qb7PFJdfZ9POWz7su6KoO8S5UtB03oNS/Kr09aixXawFIfEk3LBK", new Guid("d8a15fcf-d43e-45cc-a249-9eb0ea1761bc") },
                    { new Guid("a4b3ec4a-7707-4569-b209-ffc7137eee4c"), "nikola@gmail.com", "Nikola", "Milic", "$2a$11$JdpVOF8NvoIGkWJ8WmznhuAX/55ZSQojlrg6EnaO4BECVcyaZOYTe", new Guid("4f0849fd-5033-4d51-8e07-d5c5db42431c") }
                });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "PermissionId", "UserId" },
                values: new object[] { new Guid("1f5619cd-a38c-4eb8-aa13-8488b9995288"), new Guid("ae2799ec-722d-41fb-bba1-748f9e8a782c") });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "PermissionId", "UserId" },
                values: new object[] { new Guid("c4433744-1ffe-49d6-8de7-fd1783737e5d"), new Guid("a4b3ec4a-7707-4569-b209-ffc7137eee4c") });

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
