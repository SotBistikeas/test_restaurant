using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodCost.Migrations
{
    public partial class expences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RestaurantExpence",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reason = table.Column<string>(nullable: true),
                    MonthlyExpence = table.Column<decimal>(nullable: false),
                    RestaurantSettingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantExpence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestaurantExpence_RestaurantSettings_RestaurantSettingId",
                        column: x => x.RestaurantSettingId,
                        principalTable: "RestaurantSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantExpence_RestaurantSettingId",
                table: "RestaurantExpence",
                column: "RestaurantSettingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RestaurantExpence");
        }
    }
}
