using Microsoft.EntityFrameworkCore.Migrations;

namespace KoBooksStoreWeb.Migrations
{
    public partial class DropTempIDColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorID",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_AuthorID",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "TemporaryID",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "AuthorID",
                table: "Books",
                newName: "AuthorIDRef");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthorIDRef",
                table: "Books",
                newName: "AuthorID");

            migrationBuilder.AddColumn<int>(
                name: "TemporaryID",
                table: "Authors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorID",
                table: "Books",
                column: "AuthorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorID",
                table: "Books",
                column: "AuthorID",
                principalTable: "Authors",
                principalColumn: "AuthorID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
