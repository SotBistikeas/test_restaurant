using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodCost.Migrations
{
    public partial class add_dishes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.Sql("DELETE FROM FoodIngredient_Product_Mapping");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodIngredient_Product_Mapping_FoodIngredients_FoodIngredientId",
                table: "FoodIngredient_Product_Mapping");

            migrationBuilder.AlterColumn<int>(
                name: "FoodIngredientId",
                table: "FoodIngredient_Product_Mapping",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Dishes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    BaseCost = table.Column<decimal>(nullable: false),
                    RealCost = table.Column<decimal>(nullable: false),
                    SalePriceExclTax = table.Column<decimal>(nullable: false),
                    SalePriceInclTax = table.Column<decimal>(nullable: false),
                    UserSalePriceExclTax = table.Column<decimal>(nullable: true),
                    UserSalePriceInclTax = table.Column<decimal>(nullable: true),
                    CreatorUserId = table.Column<long>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dish_FoodIngredient_Mapping",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DishId = table.Column<int>(nullable: false),
                    FoodIngredientId = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    UnitOfMeasureId = table.Column<int>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dish_FoodIngredient_Mapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dish_FoodIngredient_Mapping_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dish_FoodIngredient_Mapping_FoodIngredients_FoodIngredientId",
                        column: x => x.FoodIngredientId,
                        principalTable: "FoodIngredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dish_FoodIngredient_Mapping_UnitOfMeasures_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalTable: "UnitOfMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dish_FoodIngredient_Mapping_DishId",
                table: "Dish_FoodIngredient_Mapping",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_Dish_FoodIngredient_Mapping_FoodIngredientId",
                table: "Dish_FoodIngredient_Mapping",
                column: "FoodIngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Dish_FoodIngredient_Mapping_UnitOfMeasureId",
                table: "Dish_FoodIngredient_Mapping",
                column: "UnitOfMeasureId");




            migrationBuilder.AddForeignKey(
                name: "FK_FoodIngredient_Product_Mapping_FoodIngredients_FoodIngredientId",
                table: "FoodIngredient_Product_Mapping",
                column: "FoodIngredientId",
                principalTable: "FoodIngredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodIngredient_Product_Mapping_FoodIngredients_FoodIngredientId",
                table: "FoodIngredient_Product_Mapping");

            migrationBuilder.DropTable(
                name: "Dish_FoodIngredient_Mapping");

            migrationBuilder.DropTable(
                name: "Dishes");

            migrationBuilder.AlterColumn<int>(
                name: "FoodIngredientId",
                table: "FoodIngredient_Product_Mapping",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_FoodIngredient_Product_Mapping_FoodIngredients_FoodIngredientId",
                table: "FoodIngredient_Product_Mapping",
                column: "FoodIngredientId",
                principalTable: "FoodIngredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
