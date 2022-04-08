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
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("57677386-3999-4011-ba64-085b2f321e3f"))
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
                    { new Guid("723bb965-8689-4934-a9a2-5442942e5473"), "UserGetAll" },
                    { new Guid("3b342250-a052-4d8f-8433-08c45f258d02"), "UserDelete" },
                    { new Guid("dbf4f926-0ea6-4c70-971d-6f87ef4d4aa2"), "UserUpdate" },
                    { new Guid("b37105b5-f801-4218-af95-47c4b61f07c6"), "UserGetById" },
                    { new Guid("44114348-5c6f-444e-88cb-0e36369dbb53"), "UserCreateUser" },
                    { new Guid("7daa70f5-9de0-4a73-974e-03c2b8f33637"), "IncidentsGetAll" },
                    { new Guid("d6bead18-e752-4d9e-bf85-fe6cda2b2754"), "IncidentsGetById" },
                    { new Guid("6bf02553-ea87-4b11-95b3-a853d7fdc58f"), "IncidentsUpdate" },
                    { new Guid("3202b090-4d7e-4049-81d6-4e5c909288c9"), "IncidentsDelete" },
                    { new Guid("65a007bb-221e-49fc-8201-37d6c1e11b8c"), "IncidentsCreate" },
                    { new Guid("968852ea-a13f-4896-9bb6-be970c3ca2f1"), "PromoteToAdmin" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "UserType" },
                values: new object[,]
                {
                    { new Guid("57677386-3999-4011-ba64-085b2f321e3f"), "User" },
                    { new Guid("c16c32eb-95ae-45e3-8641-6304d8d8759e"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "Password", "RoleId" },
                values: new object[,]
                {
                    { new Guid("aa4cf009-44b7-4413-89dc-00902bdd3bf7"), "marko@gmail.com", "Marko", "Milic", "123456", new Guid("57677386-3999-4011-ba64-085b2f321e3f") },
                    { new Guid("e16e702f-cbcf-47a5-a2ed-e2ded8aeb5c3"), "Nikola@gmail.com", "Nikola", "Milic", "123456", new Guid("c16c32eb-95ae-45e3-8641-6304d8d8759e") }
                });

            migrationBuilder.InsertData(
                table: "rolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("7daa70f5-9de0-4a73-974e-03c2b8f33637"), new Guid("57677386-3999-4011-ba64-085b2f321e3f") },
                    { new Guid("7daa70f5-9de0-4a73-974e-03c2b8f33637"), new Guid("c16c32eb-95ae-45e3-8641-6304d8d8759e") },
                    { new Guid("d6bead18-e752-4d9e-bf85-fe6cda2b2754"), new Guid("c16c32eb-95ae-45e3-8641-6304d8d8759e") },
                    { new Guid("6bf02553-ea87-4b11-95b3-a853d7fdc58f"), new Guid("c16c32eb-95ae-45e3-8641-6304d8d8759e") },
                    { new Guid("3202b090-4d7e-4049-81d6-4e5c909288c9"), new Guid("c16c32eb-95ae-45e3-8641-6304d8d8759e") }
                });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "PermissionId", "UserId" },
                values: new object[] { new Guid("3202b090-4d7e-4049-81d6-4e5c909288c9"), new Guid("aa4cf009-44b7-4413-89dc-00902bdd3bf7") });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "PermissionId", "UserId" },
                values: new object[] { new Guid("65a007bb-221e-49fc-8201-37d6c1e11b8c"), new Guid("e16e702f-cbcf-47a5-a2ed-e2ded8aeb5c3") });

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
