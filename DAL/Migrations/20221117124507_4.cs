using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MounthAndYear",
                table: "CarFuelConsumptions",
                newName: "Year");

            migrationBuilder.AddColumn<string>(
                name: "Mounth",
                table: "CarFuelConsumptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CarFuelConsumptions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Mounth", "Year" },
                values: new object[] { "январь", "2022" });

            migrationBuilder.UpdateData(
                table: "CarFuelConsumptions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Mounth", "Year" },
                values: new object[] { "январь", "2022" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mounth",
                table: "CarFuelConsumptions");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "CarFuelConsumptions",
                newName: "MounthAndYear");

            migrationBuilder.UpdateData(
                table: "CarFuelConsumptions",
                keyColumn: "Id",
                keyValue: 1,
                column: "MounthAndYear",
                value: "январь 2022");

            migrationBuilder.UpdateData(
                table: "CarFuelConsumptions",
                keyColumn: "Id",
                keyValue: 2,
                column: "MounthAndYear",
                value: "январь 2022");
        }
    }
}
