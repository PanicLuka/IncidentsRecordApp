using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserService.Migrations
{
    public partial class newMigration : Migration
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
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("ac58b954-9521-4416-8b64-e8f42b297afd"))
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
                    { new Guid("9dca1b7b-df23-4cb4-a827-2bd4edc8062f"), "UserGetAll" },
                    { new Guid("8ef2df52-1bd4-4983-b1f0-16a96a28b17d"), "UserDelete" },
                    { new Guid("5e9db9d3-2bba-4fc8-adc5-e654e51dd4f3"), "UserUpdate" },
                    { new Guid("34841212-ff54-42d2-8fc7-15cc4ef5c664"), "UserGetById" },
                    { new Guid("60508954-c6c1-48c7-b1d3-5e9ec4fc3439"), "UserCreateUser" },
                    { new Guid("8cd39687-7d78-4272-a7c3-43f78ae13b23"), "IncidentsGetAll" },
                    { new Guid("c49d33ae-1d20-4bbc-bf0e-f2a1bdf10b0d"), "IncidentsGetById" },
                    { new Guid("ac8dbcf9-eb93-472b-ae1a-55492dd013eb"), "IncidentsUpdate" },
                    { new Guid("facb87f7-f189-46a5-b434-629edd44d7f2"), "IncidentsDelete" },
                    { new Guid("1e780ba9-d58c-4878-b69a-a2588ba93af2"), "IncidentsCreate" },
                    { new Guid("2dd89718-a3fc-40e4-b140-936b5bfc6e24"), "PromoteToAdmin" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "UserType" },
                values: new object[,]
                {
                    { new Guid("ac58b954-9521-4416-8b64-e8f42b297afd"), "User" },
                    { new Guid("02e05a56-3c26-4bbc-bfa3-2d6e696b364d"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("8cd39687-7d78-4272-a7c3-43f78ae13b23"), new Guid("ac58b954-9521-4416-8b64-e8f42b297afd") },
                    { new Guid("8cd39687-7d78-4272-a7c3-43f78ae13b23"), new Guid("02e05a56-3c26-4bbc-bfa3-2d6e696b364d") },
                    { new Guid("c49d33ae-1d20-4bbc-bf0e-f2a1bdf10b0d"), new Guid("02e05a56-3c26-4bbc-bfa3-2d6e696b364d") },
                    { new Guid("ac8dbcf9-eb93-472b-ae1a-55492dd013eb"), new Guid("02e05a56-3c26-4bbc-bfa3-2d6e696b364d") },
                    { new Guid("facb87f7-f189-46a5-b434-629edd44d7f2"), new Guid("02e05a56-3c26-4bbc-bfa3-2d6e696b364d") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "Password", "RoleId" },
                values: new object[,]
                {
                    { new Guid("d63aa8e8-7b20-4290-9f6d-7ed77cce7290"), "marko@gmail.com", "Marko", "Milic", "$2a$11$PhR6MTU8hBwVJsB1qVGbTOU1gCXU08MRzOStIAqoTq/WZLCbXRFKu", new Guid("ac58b954-9521-4416-8b64-e8f42b297afd") },
                    { new Guid("cac1a4bc-7468-41e3-a213-30371559abca"), "nikola@gmail.com", "Nikola", "Milic", "$2a$11$JsaYHicNKu1PlyIcFO8bSeYjgIDyr9VkD4i6Cq1mZcBFgxVvYHi86", new Guid("02e05a56-3c26-4bbc-bfa3-2d6e696b364d") }
                });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "PermissionId", "UserId" },
                values: new object[] { new Guid("facb87f7-f189-46a5-b434-629edd44d7f2"), new Guid("d63aa8e8-7b20-4290-9f6d-7ed77cce7290") });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "PermissionId", "UserId" },
                values: new object[] { new Guid("1e780ba9-d58c-4878-b69a-a2588ba93af2"), new Guid("cac1a4bc-7468-41e3-a213-30371559abca") });

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
