using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodCost.Migrations
{
    public partial class AutomaticFoodIngredient2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAutomatic",
                table: "FoodIngredients",
                newName: "IsProduct");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsProduct",
                table: "FoodIngredients",
                newName: "IsAutomatic");
        }
    }
}
