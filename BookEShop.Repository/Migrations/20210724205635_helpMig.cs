using Microsoft.EntityFrameworkCore.Migrations;

namespace BookEShop.Web.Data.Migrations
{
    public partial class helpMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearPublished",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "YearPublished",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
