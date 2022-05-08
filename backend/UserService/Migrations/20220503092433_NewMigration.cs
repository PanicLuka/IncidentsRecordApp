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
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("856a900e-b033-46bb-853e-e400b65848ee"))
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
                    { new Guid("f0c38bf6-4953-4faa-b1de-7f0ca43a3158"), "UserGetAll" },
                    { new Guid("ed26b6cf-e434-417b-990d-f61920c0ebac"), "UserDelete" },
                    { new Guid("88af7d32-ee30-4627-9d9f-e565bd41ffc8"), "UserUpdate" },
                    { new Guid("3214d45e-c75a-4ce1-9612-2a9526d6084d"), "UserGetById" },
                    { new Guid("aa503b6c-ef8d-4cb8-b883-96f870dbe81a"), "UserCreateUser" },
                    { new Guid("63757663-cfce-4527-bafd-d87ae0c18061"), "IncidentsGetAll" },
                    { new Guid("530252cb-3bf6-439d-9b32-aef0a41fa653"), "IncidentsGetById" },
                    { new Guid("938011c5-e67e-43d5-9038-5b01f837b96f"), "IncidentsUpdate" },
                    { new Guid("58cb9d88-49da-4a76-832a-4c2db4d900e9"), "IncidentsDelete" },
                    { new Guid("1e6862c8-f8e5-4566-a395-1116f5e3d53a"), "IncidentsCreate" },
                    { new Guid("b8cb3860-b899-4575-9aee-6fff9cc1271e"), "PromoteToAdmin" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "UserType" },
                values: new object[,]
                {
                    { new Guid("856a900e-b033-46bb-853e-e400b65848ee"), "User" },
                    { new Guid("ab439b71-e326-4794-af4d-c5d7ed946640"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("63757663-cfce-4527-bafd-d87ae0c18061"), new Guid("856a900e-b033-46bb-853e-e400b65848ee") },
                    { new Guid("63757663-cfce-4527-bafd-d87ae0c18061"), new Guid("ab439b71-e326-4794-af4d-c5d7ed946640") },
                    { new Guid("530252cb-3bf6-439d-9b32-aef0a41fa653"), new Guid("ab439b71-e326-4794-af4d-c5d7ed946640") },
                    { new Guid("938011c5-e67e-43d5-9038-5b01f837b96f"), new Guid("ab439b71-e326-4794-af4d-c5d7ed946640") },
                    { new Guid("58cb9d88-49da-4a76-832a-4c2db4d900e9"), new Guid("ab439b71-e326-4794-af4d-c5d7ed946640") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "Password", "RoleId" },
                values: new object[,]
                {
                    { new Guid("d26f54b3-e932-4b0d-868b-711e503ff498"), "marko@gmail.com", "Marko", "Milic", "Test12345", new Guid("856a900e-b033-46bb-853e-e400b65848ee") },
                    { new Guid("8ebe5aac-9b20-4653-82c8-f75fcbdf7200"), "nikola@gmail.com", "Nikola", "Milic", "Test12345", new Guid("ab439b71-e326-4794-af4d-c5d7ed946640") }
                });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "PermissionId", "UserId" },
                values: new object[] { new Guid("58cb9d88-49da-4a76-832a-4c2db4d900e9"), new Guid("d26f54b3-e932-4b0d-868b-711e503ff498") });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "PermissionId", "UserId" },
                values: new object[] { new Guid("1e6862c8-f8e5-4566-a395-1116f5e3d53a"), new Guid("8ebe5aac-9b20-4653-82c8-f75fcbdf7200") });

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
