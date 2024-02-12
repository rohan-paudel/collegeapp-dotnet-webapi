using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeAppDotnetWebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedQueryModelColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueryModel_AspNetUsers_UserId",
                table: "QueryModel");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "QueryModel",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_QueryModel_UserId",
                table: "QueryModel",
                newName: "IX_QueryModel_StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_QueryModel_AspNetUsers_StudentId",
                table: "QueryModel",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueryModel_AspNetUsers_StudentId",
                table: "QueryModel");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "QueryModel",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_QueryModel_StudentId",
                table: "QueryModel",
                newName: "IX_QueryModel_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_QueryModel_AspNetUsers_UserId",
                table: "QueryModel",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
