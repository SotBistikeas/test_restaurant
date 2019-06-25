using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodCost.Migrations
{
    public partial class VatCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Products",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Products",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "VatCategoryId",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "VatCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Vat = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VatCategories", x => x.Id);
                });

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
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_VatCategories_VatCategoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "VatCategories");

            migrationBuilder.DropIndex(
                name: "IX_Products_VatCategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "VatCategoryId",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);
        }
    }
}
