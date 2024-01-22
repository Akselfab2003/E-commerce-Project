using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce.Logic.Migrations
{
    /// <inheritdoc />
    public partial class AddedPriceListEntitytoPriceListandaddedlistofCompaniesandusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceList_Products_ParentProductIdId",
                table: "PriceList");

            migrationBuilder.DropIndex(
                name: "IX_PriceList_ParentProductIdId",
                table: "PriceList");

            migrationBuilder.DropColumn(
                name: "ParentProductIdId",
                table: "PriceList");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "PriceList");

            migrationBuilder.AddColumn<int>(
                name: "PriceListId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PriceListId",
                table: "Company",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "priceListEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriceListPrice = table.Column<double>(type: "float", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    PriceListId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_priceListEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_priceListEntities_PriceList_PriceListId",
                        column: x => x.PriceListId,
                        principalTable: "PriceList",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_priceListEntities_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_PriceListId",
                table: "Users",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_PriceListId",
                table: "Company",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_priceListEntities_PriceListId",
                table: "priceListEntities",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_priceListEntities_ProductId",
                table: "priceListEntities",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_PriceList_PriceListId",
                table: "Company",
                column: "PriceListId",
                principalTable: "PriceList",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PriceList_PriceListId",
                table: "Users",
                column: "PriceListId",
                principalTable: "PriceList",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_PriceList_PriceListId",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_PriceList_PriceListId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "priceListEntities");

            migrationBuilder.DropIndex(
                name: "IX_Users_PriceListId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Company_PriceListId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "PriceListId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PriceListId",
                table: "Company");

            migrationBuilder.AddColumn<int>(
                name: "ParentProductIdId",
                table: "PriceList",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "PriceList",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PriceList_ParentProductIdId",
                table: "PriceList",
                column: "ParentProductIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceList_Products_ParentProductIdId",
                table: "PriceList",
                column: "ParentProductIdId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
