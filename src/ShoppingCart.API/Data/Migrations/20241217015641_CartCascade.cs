using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCart.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class CartCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemCart_CustomerCart_CartId",
                table: "ItemCart");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCart_CustomerCart_CartId",
                table: "ItemCart",
                column: "CartId",
                principalTable: "CustomerCart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemCart_CustomerCart_CartId",
                table: "ItemCart");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCart_CustomerCart_CartId",
                table: "ItemCart",
                column: "CartId",
                principalTable: "CustomerCart",
                principalColumn: "Id");
        }
    }
}
