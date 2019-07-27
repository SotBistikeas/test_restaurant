using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodCost.Migrations
{
    public partial class UnitOfMeasure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
migrationBuilder.Sql(@"
UPDATE [UnitOfMeasures] SET [MeasureGroupId] = 1 WHERE ID in (310, 314, 320 )
");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
