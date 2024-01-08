using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce.Logic.Migrations
{
    /// <inheritdoc />
    public partial class Mademinorchangestotherelationshipbetweenthetables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_Sessions_SessionId",
                table: "Basket");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketDetails_Users_BasketIdId",
                table: "BasketDetails");

            migrationBuilder.DropIndex(
                name: "IX_Basket_SessionId",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Basket");

            migrationBuilder.RenameColumn(
                name: "BasketIdId",
                table: "BasketDetails",
                newName: "BasketId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketDetails_BasketIdId",
                table: "BasketDetails",
                newName: "IX_BasketDetails_BasketId");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Gender",
                table: "Users",
                type: "Binary(1)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "Binary");

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "orderId",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BasketDetailsId",
                table: "DiscountCodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_SessionId",
                table: "Users",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_orderId",
                table: "OrderDetails",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCodes_BasketDetailsId",
                table: "DiscountCodes",
                column: "BasketDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_UserId",
                table: "Company",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketDetails_Basket_BasketId",
                table: "BasketDetails",
                column: "BasketId",
                principalTable: "Basket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Users_UserId",
                table: "Company",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountCodes_BasketDetails_BasketDetailsId",
                table: "DiscountCodes",
                column: "BasketDetailsId",
                principalTable: "BasketDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_orderId",
                table: "OrderDetails",
                column: "orderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Sessions_SessionId",
                table: "Users",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketDetails_Basket_BasketId",
                table: "BasketDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_Users_UserId",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountCodes_BasketDetails_BasketDetailsId",
                table: "DiscountCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_orderId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Sessions_SessionId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_SessionId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_orderId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_DiscountCodes_BasketDetailsId",
                table: "DiscountCodes");

            migrationBuilder.DropIndex(
                name: "IX_Company_UserId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "orderId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "BasketDetailsId",
                table: "DiscountCodes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Company");

            migrationBuilder.RenameColumn(
                name: "BasketId",
                table: "BasketDetails",
                newName: "BasketIdId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketDetails_BasketId",
                table: "BasketDetails",
                newName: "IX_BasketDetails_BasketIdId");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Gender",
                table: "Users",
                type: "Binary",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "Binary(1)");

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
                name: "FK_BasketDetails_Users_BasketIdId",
                table: "BasketDetails",
                column: "BasketIdId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
