using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodCost.Migrations
{
    public partial class BaseEquivalentPersision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "BaseEquivalent",
                table: "UnitOfMeasures",
                type: "decimal(18, 6)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.Sql(@"
UPDATE UnitOfMeasures SET BaseEquivalent = 1.0 WHERE Id = 101;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.001 WHERE Id = 102;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.01 WHERE Id = 103;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.1 WHERE Id = 104;
UPDATE UnitOfMeasures SET BaseEquivalent = 1000.0 WHERE Id = 105;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.254 WHERE Id = 106;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.3048 WHERE Id = 107;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.9144 WHERE Id = 108;
UPDATE UnitOfMeasures SET BaseEquivalent = 1609.0 WHERE Id = 109;
UPDATE UnitOfMeasures SET BaseEquivalent = 1852.0 WHERE Id = 110;
UPDATE UnitOfMeasures SET BaseEquivalent = 1.0 WHERE Id = 201;
UPDATE UnitOfMeasures SET BaseEquivalent = 4046.873 WHERE Id = 202;
UPDATE UnitOfMeasures SET BaseEquivalent = 100.0 WHERE Id = 203;
UPDATE UnitOfMeasures SET BaseEquivalent = 10000.0 WHERE Id = 204;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.00064516 WHERE Id = 205;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.093 WHERE Id = 206;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.836 WHERE Id = 207;
UPDATE UnitOfMeasures SET BaseEquivalent = 2590000.0 WHERE Id = 208;
UPDATE UnitOfMeasures SET BaseEquivalent = 1000.0 WHERE Id = 301;
UPDATE UnitOfMeasures SET BaseEquivalent = 1.0 WHERE Id = 302;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.001 WHERE Id = 303;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.01 WHERE Id = 304;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.1 WHERE Id = 305;
UPDATE UnitOfMeasures SET BaseEquivalent = 100.0 WHERE Id = 306;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.016387 WHERE Id = 307;
UPDATE UnitOfMeasures SET BaseEquivalent = 28.317 WHERE Id = 308;
UPDATE UnitOfMeasures SET BaseEquivalent = 764.555 WHERE Id = 309;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.00591939 WHERE Id = 310;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.00493 WHERE Id = 311;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.0177582 WHERE Id = 312;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.0148 WHERE Id = 313;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.02 WHERE Id = 314;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.015 WHERE Id = 315;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.283872 WHERE Id = 316;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.2957 WHERE Id = 317;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.28413 WHERE Id = 318;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.23659 WHERE Id = 319;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.25 WHERE Id = 320;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.142 WHERE Id = 321;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.118 WHERE Id = 322;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.138 WHERE Id = 323;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.568 WHERE Id = 324;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.473 WHERE Id = 325;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.551 WHERE Id = 326;
UPDATE UnitOfMeasures SET BaseEquivalent = 1.14 WHERE Id = 327;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.946 WHERE Id = 328;
UPDATE UnitOfMeasures SET BaseEquivalent = 1.101 WHERE Id = 329;
UPDATE UnitOfMeasures SET BaseEquivalent = 4.55 WHERE Id = 330;
UPDATE UnitOfMeasures SET BaseEquivalent = 3.78 WHERE Id = 331;
UPDATE UnitOfMeasures SET BaseEquivalent = 4.4 WHERE Id = 332;
UPDATE UnitOfMeasures SET BaseEquivalent = 1.0 WHERE Id = 401;
UPDATE UnitOfMeasures SET BaseEquivalent = 1000.0 WHERE Id = 402;
UPDATE UnitOfMeasures SET BaseEquivalent = 0.06479886 WHERE Id = 403;
UPDATE UnitOfMeasures SET BaseEquivalent = 1.77184375 WHERE Id = 404;
UPDATE UnitOfMeasures SET BaseEquivalent = 28.3495 WHERE Id = 405;
UPDATE UnitOfMeasures SET BaseEquivalent = 453.592 WHERE Id = 406;
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "BaseEquivalent",
                table: "UnitOfMeasures",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 6)");
        }
    }
}
