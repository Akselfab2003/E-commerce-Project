using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce.Logic.Migrations
{
    /// <inheritdoc />
    public partial class AddedProductVariantstoOrderDetailsmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReviewRating",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReviewTitle",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "variantId",
                table: "OrderDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_variantId",
                table: "OrderDetails",
                column: "variantId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_ProductVariants_variantId",
                table: "OrderDetails",
                column: "variantId",
                principalTable: "ProductVariants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_ProductVariants_variantId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_variantId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ReviewRating",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ReviewTitle",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "variantId",
                table: "OrderDetails");
        }
    }
}
