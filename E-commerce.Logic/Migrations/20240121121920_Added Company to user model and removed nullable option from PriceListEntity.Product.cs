using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce.Logic.Migrations
{
    /// <inheritdoc />
    public partial class AddedCompanytousermodelandremovednullableoptionfromPriceListEntityProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Users_UserId",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_priceListEntities_Products_ProductId",
                table: "priceListEntities");

            migrationBuilder.DropIndex(
                name: "IX_Company_UserId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Company");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "priceListEntities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId",
                table: "Users",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_priceListEntities_Products_ProductId",
                table: "priceListEntities",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Company_CompanyId",
                table: "Users",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_priceListEntities_Products_ProductId",
                table: "priceListEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Company_CompanyId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CompanyId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "priceListEntities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Company_UserId",
                table: "Company",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Users_UserId",
                table: "Company",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_priceListEntities_Products_ProductId",
                table: "priceListEntities",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
