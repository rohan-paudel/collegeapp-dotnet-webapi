using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeAppDotnetWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedIndexInNoteModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TejiloCollege",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "NoteModel",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TejiloCollege_Name",
                table: "TejiloCollege",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_TejiloCollege_Status",
                table: "TejiloCollege",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_NoteModel_Name",
                table: "NoteModel",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_NoteModel_Status",
                table: "NoteModel",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TejiloCollege_Name",
                table: "TejiloCollege");

            migrationBuilder.DropIndex(
                name: "IX_TejiloCollege_Status",
                table: "TejiloCollege");

            migrationBuilder.DropIndex(
                name: "IX_NoteModel_Name",
                table: "NoteModel");

            migrationBuilder.DropIndex(
                name: "IX_NoteModel_Status",
                table: "NoteModel");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TejiloCollege",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "NoteModel",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
