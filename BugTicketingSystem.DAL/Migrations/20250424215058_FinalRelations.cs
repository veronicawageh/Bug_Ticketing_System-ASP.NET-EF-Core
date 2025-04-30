using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTicketingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FinalRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bug_Projects_ProjectId",
                table: "Bug");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBug_Bug_BugId",
                table: "UserBug");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBug_Users_UserId",
                table: "UserBug");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBug",
                table: "UserBug");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bug",
                table: "Bug");

            migrationBuilder.RenameTable(
                name: "UserBug",
                newName: "userBugs");

            migrationBuilder.RenameTable(
                name: "Bug",
                newName: "Bugs");

            migrationBuilder.RenameIndex(
                name: "IX_UserBug_BugId",
                table: "userBugs",
                newName: "IX_userBugs_BugId");

            migrationBuilder.RenameIndex(
                name: "IX_Bug_ProjectId",
                table: "Bugs",
                newName: "IX_Bugs_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userBugs",
                table: "userBugs",
                columns: new[] { "UserId", "BugId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bugs",
                table: "Bugs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Path = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BugId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Bugs_BugId",
                        column: x => x.BugId,
                        principalTable: "Bugs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_BugId",
                table: "Files",
                column: "BugId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_Projects_ProjectId",
                table: "Bugs",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userBugs_Bugs_BugId",
                table: "userBugs",
                column: "BugId",
                principalTable: "Bugs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userBugs_Users_UserId",
                table: "userBugs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_Projects_ProjectId",
                table: "Bugs");

            migrationBuilder.DropForeignKey(
                name: "FK_userBugs_Bugs_BugId",
                table: "userBugs");

            migrationBuilder.DropForeignKey(
                name: "FK_userBugs_Users_UserId",
                table: "userBugs");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userBugs",
                table: "userBugs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bugs",
                table: "Bugs");

            migrationBuilder.RenameTable(
                name: "userBugs",
                newName: "UserBug");

            migrationBuilder.RenameTable(
                name: "Bugs",
                newName: "Bug");

            migrationBuilder.RenameIndex(
                name: "IX_userBugs_BugId",
                table: "UserBug",
                newName: "IX_UserBug_BugId");

            migrationBuilder.RenameIndex(
                name: "IX_Bugs_ProjectId",
                table: "Bug",
                newName: "IX_Bug_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBug",
                table: "UserBug",
                columns: new[] { "UserId", "BugId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bug",
                table: "Bug",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bug_Projects_ProjectId",
                table: "Bug",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBug_Bug_BugId",
                table: "UserBug",
                column: "BugId",
                principalTable: "Bug",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBug_Users_UserId",
                table: "UserBug",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
