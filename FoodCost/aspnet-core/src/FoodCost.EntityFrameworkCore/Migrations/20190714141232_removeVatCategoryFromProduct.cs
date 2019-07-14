using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodCost.Migrations
{
    public partial class removeVatCategoryFromProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_VatCategories_VatCategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_VatCategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "VatCategoryId",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VatCategoryId",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_VatCategoryId",
                table: "Products",
                column: "VatCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_VatCategories_VatCategoryId",
                table: "Products",
                column: "VatCategoryId",
                principalTable: "VatCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
