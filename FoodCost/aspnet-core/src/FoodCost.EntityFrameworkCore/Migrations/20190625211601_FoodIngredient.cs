using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodCost.Migrations
{
    public partial class FoodIngredient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    UnitOfMeasureId = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodIngredients_UnitOfMeasures_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalTable: "UnitOfMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodIngredient_Product_Mapping",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    FoodIngredientId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodIngredient_Product_Mapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodIngredient_Product_Mapping_FoodIngredients_FoodIngredientId",
                        column: x => x.FoodIngredientId,
                        principalTable: "FoodIngredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FoodIngredient_Product_Mapping_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodIngredient_Product_Mapping_FoodIngredientId",
                table: "FoodIngredient_Product_Mapping",
                column: "FoodIngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodIngredient_Product_Mapping_ProductId",
                table: "FoodIngredient_Product_Mapping",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodIngredients_UnitOfMeasureId",
                table: "FoodIngredients",
                column: "UnitOfMeasureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodIngredient_Product_Mapping");

            migrationBuilder.DropTable(
                name: "FoodIngredients");
        }
    }
}
