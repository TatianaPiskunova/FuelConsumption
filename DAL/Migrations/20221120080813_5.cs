using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CarFuelConsumptions",
                keyColumn: "Id",
                keyValue: 2,
                column: "DriverId",
                value: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CarFuelConsumptions",
                keyColumn: "Id",
                keyValue: 2,
                column: "DriverId",
                value: 2);
        }
    }
}
