using Microsoft.EntityFrameworkCore.Migrations;

namespace BookEShop.Web.Data.Migrations
{
    public partial class UpdateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketInShoppingCarts_ShoppingCarts_BookId",
                table: "TicketInShoppingCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketInShoppingCarts_Books_ShoppingCartId",
                table: "TicketInShoppingCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketInShoppingCarts",
                table: "TicketInShoppingCarts");

            migrationBuilder.RenameTable(
                name: "TicketInShoppingCarts",
                newName: "BookInShoppingCarts");

            migrationBuilder.RenameIndex(
                name: "IX_TicketInShoppingCarts_ShoppingCartId",
                table: "BookInShoppingCarts",
                newName: "IX_BookInShoppingCarts_ShoppingCartId");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "BookInShoppingCarts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookInShoppingCarts",
                table: "BookInShoppingCarts",
                columns: new[] { "BookId", "ShoppingCartId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookInShoppingCarts_ShoppingCarts_BookId",
                table: "BookInShoppingCarts",
                column: "BookId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookInShoppingCarts_Books_ShoppingCartId",
                table: "BookInShoppingCarts",
                column: "ShoppingCartId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookInShoppingCarts_ShoppingCarts_BookId",
                table: "BookInShoppingCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_BookInShoppingCarts_Books_ShoppingCartId",
                table: "BookInShoppingCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookInShoppingCarts",
                table: "BookInShoppingCarts");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "BookInShoppingCarts");

            migrationBuilder.RenameTable(
                name: "BookInShoppingCarts",
                newName: "TicketInShoppingCarts");

            migrationBuilder.RenameIndex(
                name: "IX_BookInShoppingCarts_ShoppingCartId",
                table: "TicketInShoppingCarts",
                newName: "IX_TicketInShoppingCarts_ShoppingCartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketInShoppingCarts",
                table: "TicketInShoppingCarts",
                columns: new[] { "BookId", "ShoppingCartId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TicketInShoppingCarts_ShoppingCarts_BookId",
                table: "TicketInShoppingCarts",
                column: "BookId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketInShoppingCarts_Books_ShoppingCartId",
                table: "TicketInShoppingCarts",
                column: "ShoppingCartId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
