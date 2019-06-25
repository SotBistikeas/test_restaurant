using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodCost.Migrations
{
    public partial class FoodIngredient2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "FoodIngredient_Product_Mapping",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UnitOfMeasureId",
                table: "FoodIngredient_Product_Mapping",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FoodIngredient_Product_Mapping_UnitOfMeasureId",
                table: "FoodIngredient_Product_Mapping",
                column: "UnitOfMeasureId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodIngredient_Product_Mapping_UnitOfMeasures_UnitOfMeasureId",
                table: "FoodIngredient_Product_Mapping",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodIngredient_Product_Mapping_UnitOfMeasures_UnitOfMeasureId",
                table: "FoodIngredient_Product_Mapping");

            migrationBuilder.DropIndex(
                name: "IX_FoodIngredient_Product_Mapping_UnitOfMeasureId",
                table: "FoodIngredient_Product_Mapping");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "FoodIngredient_Product_Mapping");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "FoodIngredient_Product_Mapping");
        }
    }
}
