using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce.Logic.Migrations
{
    /// <inheritdoc />
    public partial class AlloweedParentProducttobenullableasnoteveryproductnecesarrilyneedsatag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Products_ParentProductIdId",
                table: "Tags");

            migrationBuilder.RenameColumn(
                name: "ParentProductIdId",
                table: "Tags",
                newName: "ParentProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_ParentProductIdId",
                table: "Tags",
                newName: "IX_Tags_ParentProductId");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Gender",
                table: "Users",
                type: "Binary(1)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "Binary");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Products_ParentProductId",
                table: "Tags",
                column: "ParentProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Products_ParentProductId",
                table: "Tags");

            migrationBuilder.RenameColumn(
                name: "ParentProductId",
                table: "Tags",
                newName: "ParentProductIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_ParentProductId",
                table: "Tags",
                newName: "IX_Tags_ParentProductIdId");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Gender",
                table: "Users",
                type: "Binary",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "Binary(1)");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Products_ParentProductIdId",
                table: "Tags",
                column: "ParentProductIdId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
