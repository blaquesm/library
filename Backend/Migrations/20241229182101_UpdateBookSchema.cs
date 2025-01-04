using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "Books",
                newName: "IsReserved");

            migrationBuilder.AddColumn<bool>(
                name: "IsIssued",
                table: "Books",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsIssued",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "IsReserved",
                table: "Books",
                newName: "IsAvailable");

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "Books",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
