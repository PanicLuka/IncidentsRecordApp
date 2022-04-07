using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserService.Migrations
{
    public partial class UserMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessPermissions = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.PermissionId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "rolePermissions",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rolePermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_rolePermissions_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rolePermissions_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("487ff1f2-af6d-4d76-8aca-0c54b90880f5"))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
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
                        name: "FK_UserPermissions_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermissions_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "PermissionId", "AccessPermissions" },
                values: new object[,]
                {
                    { new Guid("ac305f0a-ec91-4c46-a5fb-493c2dc88c59"), "CanAccessGetAllUsers" },
                    { new Guid("530204ff-0701-46c8-bd34-34c8c0e8fea6"), "CanAccessDeleteUser" },
                    { new Guid("e58d35bb-0368-489a-b0c7-29a0a799bc82"), "CanAccessUpdateUser" },
                    { new Guid("0c14e988-d157-40e7-b7d8-346abcf5aac3"), "CanAccessGetByIdUser" },
                    { new Guid("98fe9a1f-dde1-49b6-a69f-d1acd0e1e14f"), "CanAccessCreateUser" },
                    { new Guid("74836c59-2d06-4e3a-abe1-43147a787016"), "CanAccessGetAllIncidents" },
                    { new Guid("9f85d078-64ce-4a25-b27a-05c8aede3746"), "CanAccessGetIncidentById" },
                    { new Guid("1dc7ee0f-7ddd-4fe0-93e7-cdcc38746c2e"), "CanAccessUpdateIncident" },
                    { new Guid("b2206c80-3d84-4e8f-904a-3d0ffbba5f06"), "CanAccessDeleteIncident" },
                    { new Guid("6091f2f7-6e0e-4cb6-a7a8-575ee5a00fdf"), "CanAccessCreateIncident" },
                    { new Guid("d98b342a-9d28-459c-b68f-830b268430ed"), "CanPromoteToAdmin" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "UserType" },
                values: new object[,]
                {
                    { new Guid("487ff1f2-af6d-4d76-8aca-0c54b90880f5"), "User" },
                    { new Guid("22b40e6c-59d5-4d80-93a5-d15b4201ade5"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "Password", "RoleId" },
                values: new object[,]
                {
                    { new Guid("c5c49a80-5142-4e54-9d17-c1681dfa8607"), "marko@gmail.com", "Marko", "Milic", "123456", new Guid("487ff1f2-af6d-4d76-8aca-0c54b90880f5") },
                    { new Guid("c37ed033-4573-49bc-9f28-afbf995cab79"), "Nikola@gmail.com", "Nikola", "Milic", "123456", new Guid("22b40e6c-59d5-4d80-93a5-d15b4201ade5") }
                });

            migrationBuilder.InsertData(
                table: "rolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("74836c59-2d06-4e3a-abe1-43147a787016"), new Guid("487ff1f2-af6d-4d76-8aca-0c54b90880f5") },
                    { new Guid("74836c59-2d06-4e3a-abe1-43147a787016"), new Guid("22b40e6c-59d5-4d80-93a5-d15b4201ade5") },
                    { new Guid("9f85d078-64ce-4a25-b27a-05c8aede3746"), new Guid("22b40e6c-59d5-4d80-93a5-d15b4201ade5") },
                    { new Guid("1dc7ee0f-7ddd-4fe0-93e7-cdcc38746c2e"), new Guid("22b40e6c-59d5-4d80-93a5-d15b4201ade5") },
                    { new Guid("b2206c80-3d84-4e8f-904a-3d0ffbba5f06"), new Guid("22b40e6c-59d5-4d80-93a5-d15b4201ade5") }
                });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "PermissionId", "UserId" },
                values: new object[] { new Guid("b2206c80-3d84-4e8f-904a-3d0ffbba5f06"), new Guid("c5c49a80-5142-4e54-9d17-c1681dfa8607") });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "PermissionId", "UserId" },
                values: new object[] { new Guid("6091f2f7-6e0e-4cb6-a7a8-575ee5a00fdf"), new Guid("c37ed033-4573-49bc-9f28-afbf995cab79") });

            migrationBuilder.CreateIndex(
                name: "IX_rolePermissions_PermissionId",
                table: "rolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_PermissionId",
                table: "UserPermissions",
                column: "PermissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rolePermissions");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
