using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class imageadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price",
                table: "Books",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Books",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Books",
                newName: "price");
        }
    }
}
