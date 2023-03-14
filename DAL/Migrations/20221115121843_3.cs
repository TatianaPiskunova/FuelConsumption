using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WatchNumber",
                table: "CarFuelConsumptions");

            migrationBuilder.AddColumn<int>(
                name: "WatchNumberId",
                table: "CarFuelConsumptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "CarFuelConsumptions",
                keyColumn: "Id",
                keyValue: 1,
                column: "WatchNumberId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "CarFuelConsumptions",
                keyColumn: "Id",
                keyValue: 2,
                column: "WatchNumberId",
                value: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WatchNumberId",
                table: "CarFuelConsumptions");

            migrationBuilder.AddColumn<string>(
                name: "WatchNumber",
                table: "CarFuelConsumptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CarFuelConsumptions",
                keyColumn: "Id",
                keyValue: 1,
                column: "WatchNumber",
                value: "1");

            migrationBuilder.UpdateData(
                table: "CarFuelConsumptions",
                keyColumn: "Id",
                keyValue: 2,
                column: "WatchNumber",
                value: "2");
        }
    }
}
