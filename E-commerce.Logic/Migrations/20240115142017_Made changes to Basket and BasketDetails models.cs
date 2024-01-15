using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce.Logic.Migrations
{
    /// <inheritdoc />
    public partial class MadechangestoBasketandBasketDetailsmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketDetails_Basket_BasketId",
                table: "BasketDetails");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Gender",
                table: "Users",
                type: "Binary(1)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "Binary");

            migrationBuilder.AlterColumn<int>(
                name: "BasketId",
                table: "BasketDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "Basket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Basket_SessionId",
                table: "Basket",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_Sessions_SessionId",
                table: "Basket",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketDetails_Basket_BasketId",
                table: "BasketDetails",
                column: "BasketId",
                principalTable: "Basket",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_Sessions_SessionId",
                table: "Basket");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketDetails_Basket_BasketId",
                table: "BasketDetails");

            migrationBuilder.DropIndex(
                name: "IX_Basket_SessionId",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Basket");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Gender",
                table: "Users",
                type: "Binary",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "Binary(1)");

            migrationBuilder.AlterColumn<int>(
                name: "BasketId",
                table: "BasketDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketDetails_Basket_BasketId",
                table: "BasketDetails",
                column: "BasketId",
                principalTable: "Basket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
