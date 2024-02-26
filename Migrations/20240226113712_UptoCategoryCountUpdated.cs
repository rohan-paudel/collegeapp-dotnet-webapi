using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeAppDotnetWebApi.Migrations
{
    /// <inheritdoc />
    public partial class UptoCategoryCountUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryCountModel_TejiloCollege_CollegeId",
                table: "CategoryCountModel");

            migrationBuilder.DropIndex(
                name: "IX_CategoryCountModel_CollegeId",
                table: "CategoryCountModel");

            migrationBuilder.DropIndex(
                name: "IX_CategoryCountModel_CollegeId_TopicId",
                table: "CategoryCountModel");

            migrationBuilder.DropColumn(
                name: "CollegeId",
                table: "CategoryCountModel");

            migrationBuilder.DropColumn(
                name: "DiscussionCount",
                table: "CategoryCountModel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CollegeId",
                table: "CategoryCountModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiscussionCount",
                table: "CategoryCountModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryCountModel_CollegeId",
                table: "CategoryCountModel",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryCountModel_CollegeId_TopicId",
                table: "CategoryCountModel",
                columns: new[] { "CollegeId", "TopicId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryCountModel_TejiloCollege_CollegeId",
                table: "CategoryCountModel",
                column: "CollegeId",
                principalTable: "TejiloCollege",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
