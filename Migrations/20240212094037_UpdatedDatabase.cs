using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeAppDotnetWebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionModel_AspNetUsers_UserId",
                table: "DiscussionModel");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "DiscussionModel",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_DiscussionModel_UserId",
                table: "DiscussionModel",
                newName: "IX_DiscussionModel_StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionModel_AspNetUsers_StudentId",
                table: "DiscussionModel",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionModel_AspNetUsers_StudentId",
                table: "DiscussionModel");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "DiscussionModel",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DiscussionModel_StudentId",
                table: "DiscussionModel",
                newName: "IX_DiscussionModel_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionModel_AspNetUsers_UserId",
                table: "DiscussionModel",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
