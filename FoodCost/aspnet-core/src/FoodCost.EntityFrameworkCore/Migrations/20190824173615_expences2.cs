using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodCost.Migrations
{
    public partial class expences2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ServingsPerMonth",
                table: "RestaurantSettings",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServingsPerMonth",
                table: "RestaurantSettings");
        }
    }
}
