using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce.Logic.Migrations
{
    /// <inheritdoc />
    public partial class addedProductCategoriestoProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Gender",
                table: "Users",
                type: "Binary(1)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "Binary");

            migrationBuilder.AddColumn<string>(
                name: "VariantValue",
                table: "ProductVariants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProductsId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProductsId",
                table: "Categories",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Products_ProductsId",
                table: "Categories",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Products_ProductsId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ProductsId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "VariantValue",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                table: "Categories");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Gender",
                table: "Users",
                type: "Binary",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "Binary(1)");
        }
    }
}
