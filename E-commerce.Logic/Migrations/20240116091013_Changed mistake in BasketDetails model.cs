using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce.Logic.Migrations
{
    /// <inheritdoc />
    public partial class ChangedmistakeinBasketDetailsmodel : Migration
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

            migrationBuilder.AddColumn<int>(
                name: "BasketDetailsId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_BasketDetailsId",
                table: "Products",
                column: "BasketDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_BasketDetails_BasketDetailsId",
                table: "Products",
                column: "BasketDetailsId",
                principalTable: "BasketDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_BasketDetails_BasketDetailsId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BasketDetailsId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BasketDetailsId",
                table: "Products");

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
