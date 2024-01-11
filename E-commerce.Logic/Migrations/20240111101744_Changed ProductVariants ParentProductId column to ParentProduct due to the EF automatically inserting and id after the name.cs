using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce.Logic.Migrations
{
    /// <inheritdoc />
    public partial class ChangedProductVariantsParentProductIdcolumntoParentProductduetotheEFautomaticallyinsertingandidafterthename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Products_ParentProductIdId",
                table: "ProductVariants");

            migrationBuilder.RenameColumn(
                name: "ParentProductIdId",
                table: "ProductVariants",
                newName: "ParentProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariants_ParentProductIdId",
                table: "ProductVariants",
                newName: "IX_ProductVariants_ParentProductId");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Gender",
                table: "Users",
                type: "Binary(1)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "Binary");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Products_ParentProductId",
                table: "ProductVariants",
                column: "ParentProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Products_ParentProductId",
                table: "ProductVariants");

            migrationBuilder.RenameColumn(
                name: "ParentProductId",
                table: "ProductVariants",
                newName: "ParentProductIdId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariants_ParentProductId",
                table: "ProductVariants",
                newName: "IX_ProductVariants_ParentProductIdId");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Gender",
                table: "Users",
                type: "Binary",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "Binary(1)");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Products_ParentProductIdId",
                table: "ProductVariants",
                column: "ParentProductIdId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
