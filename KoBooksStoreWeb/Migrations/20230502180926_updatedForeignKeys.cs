using Microsoft.EntityFrameworkCore.Migrations;

namespace KoBooksStoreWeb.Migrations
{
    public partial class updatedForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Books_BookID",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_CartID",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_BookID",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_CartID",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "BookID",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "CartID",
                table: "CartItems");

            migrationBuilder.AddColumn<int>(
                name: "BookIDRef",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CartIDRef",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookIDRef",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "CartIDRef",
                table: "CartItems");

            migrationBuilder.AddColumn<int>(
                name: "BookID",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CartID",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_BookID",
                table: "CartItems",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartID",
                table: "CartItems",
                column: "CartID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Books_BookID",
                table: "CartItems",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "BookID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartID",
                table: "CartItems",
                column: "CartID",
                principalTable: "Carts",
                principalColumn: "CartID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
