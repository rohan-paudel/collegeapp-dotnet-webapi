using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeAppDotnetWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedDiscussionAndQueryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiscussionModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Discussion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    CollegeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscussionModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscussionModel_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscussionModel_TejiloCollege_CollegeId",
                        column: x => x.CollegeId,
                        principalTable: "TejiloCollege",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscussionModel_TopicModel_TopicId",
                        column: x => x.TopicId,
                        principalTable: "TopicModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QueryModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Query = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DiscussionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueryModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QueryModel_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QueryModel_DiscussionModel_DiscussionId",
                        column: x => x.DiscussionId,
                        principalTable: "DiscussionModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionModel_CollegeId",
                table: "DiscussionModel",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionModel_CollegeId_TopicId",
                table: "DiscussionModel",
                columns: new[] { "CollegeId", "TopicId" });

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionModel_Status",
                table: "DiscussionModel",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionModel_TopicId",
                table: "DiscussionModel",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionModel_UserId",
                table: "DiscussionModel",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QueryModel_DiscussionId",
                table: "QueryModel",
                column: "DiscussionId");

            migrationBuilder.CreateIndex(
                name: "IX_QueryModel_UserId",
                table: "QueryModel",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QueryModel");

            migrationBuilder.DropTable(
                name: "DiscussionModel");
        }
    }
}
