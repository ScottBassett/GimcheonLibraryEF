using Microsoft.EntityFrameworkCore.Migrations;

namespace GimcheonLibraryEF.DataAccess.Migrations
{
    public partial class ChangedTableNameToLibraryUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "LibraryUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LibraryUsers",
                table: "LibraryUsers",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LibraryUsers",
                table: "LibraryUsers");

            migrationBuilder.RenameTable(
                name: "LibraryUsers",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }
    }
}
