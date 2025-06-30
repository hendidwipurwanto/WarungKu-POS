using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warungku.Core.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceItemwithProductIdcolumninpostable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Item",
                table: "pointOfSales");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "pointOfSales",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "pointOfSales");

            migrationBuilder.AddColumn<string>(
                name: "Item",
                table: "pointOfSales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
