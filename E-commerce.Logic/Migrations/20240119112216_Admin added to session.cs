using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce.Logic.Migrations
{
    /// <inheritdoc />
    public partial class Adminaddedtosession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Users_userId",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Sessions",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_userId",
                table: "Sessions",
                newName: "IX_Sessions_Users");

            migrationBuilder.AddColumn<int>(
                name: "AdminUsers",
                table: "Sessions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Sessions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_AdminUsers",
                table: "Sessions",
                column: "AdminUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_AdminUsers_AdminUsers",
                table: "Sessions",
                column: "AdminUsers",
                principalTable: "AdminUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Users_Users",
                table: "Sessions",
                column: "Users",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_AdminUsers_AdminUsers",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Users_Users",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_AdminUsers",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "AdminUsers",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "Users",
                table: "Sessions",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_Users",
                table: "Sessions",
                newName: "IX_Sessions_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Users_userId",
                table: "Sessions",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
