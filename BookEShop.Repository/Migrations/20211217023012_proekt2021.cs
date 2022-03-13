using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookEShop.Web.Data.Migrations
{
    public partial class proekt2021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookInShoppingCarts",
                table: "BookInShoppingCarts");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "BookInShoppingCarts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookInShoppingCarts",
                table: "BookInShoppingCarts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookInOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BookId = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookInOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookInOrders_Orders_BookId",
                        column: x => x.BookId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookInOrders_Books_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookInShoppingCarts_BookId",
                table: "BookInShoppingCarts",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookInOrders_BookId",
                table: "BookInOrders",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookInOrders_OrderId",
                table: "BookInOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookInOrders");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookInShoppingCarts",
                table: "BookInShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_BookInShoppingCarts_BookId",
                table: "BookInShoppingCarts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BookInShoppingCarts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookInShoppingCarts",
                table: "BookInShoppingCarts",
                columns: new[] { "BookId", "ShoppingCartId" });
        }
    }
}
