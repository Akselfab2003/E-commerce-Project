using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce.Logic.Migrations
{
    /// <inheritdoc />
    public partial class Added_ProductReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductsId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductsId",
                table: "Reviews",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Products_ProductsId",
                table: "Reviews",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Products_ProductsId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ProductsId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                table: "Reviews");
        }
    }
}
