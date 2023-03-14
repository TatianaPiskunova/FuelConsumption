using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FuelConsumptionPlus_2Watch",
                table: "CarFuelConsumptions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "FuelConsumptionPlus_1Watch",
                table: "CarFuelConsumptions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "FuelConsumptionMinus_2Watch",
                table: "CarFuelConsumptions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "FuelConsumptionMinus_1Watch",
                table: "CarFuelConsumptions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "EqualFuelConsumptionLast_2Watch",
                table: "CarFuelConsumptions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "EqualFuelConsumptionLast_1Watch",
                table: "CarFuelConsumptions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "EqualFuelConsumptionFirst_2Watch",
                table: "CarFuelConsumptions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "EqualFuelConsumptionFirst_1Watch",
                table: "CarFuelConsumptions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.InsertData(
                table: "CarFuelConsumptions",
                columns: new[] { "Id", "CarId", "DriverId", "EqualFuelConsumptionFirst_1Watch", "EqualFuelConsumptionFirst_2Watch", "EqualFuelConsumptionLast_1Watch", "EqualFuelConsumptionLast_2Watch", "FuelConsumptionMinus_1Watch", "FuelConsumptionMinus_2Watch", "FuelConsumptionPlus_1Watch", "FuelConsumptionPlus_2Watch", "MounthAndYear", "WatchNumber" },
                values: new object[] { 1, 1, 1, "100", null, "50", null, "150", null, "100", null, "январь 2022", "1" });

            migrationBuilder.InsertData(
                table: "CarFuelConsumptions",
                columns: new[] { "Id", "CarId", "DriverId", "EqualFuelConsumptionFirst_1Watch", "EqualFuelConsumptionFirst_2Watch", "EqualFuelConsumptionLast_1Watch", "EqualFuelConsumptionLast_2Watch", "FuelConsumptionMinus_1Watch", "FuelConsumptionMinus_2Watch", "FuelConsumptionPlus_1Watch", "FuelConsumptionPlus_2Watch", "MounthAndYear", "WatchNumber" },
                values: new object[] { 2, 1, 2, null, "50", null, "170", null, "80", null, "200", "январь 2022", "2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarFuelConsumptions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CarFuelConsumptions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<double>(
                name: "FuelConsumptionPlus_2Watch",
                table: "CarFuelConsumptions",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "FuelConsumptionPlus_1Watch",
                table: "CarFuelConsumptions",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "FuelConsumptionMinus_2Watch",
                table: "CarFuelConsumptions",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "FuelConsumptionMinus_1Watch",
                table: "CarFuelConsumptions",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "EqualFuelConsumptionLast_2Watch",
                table: "CarFuelConsumptions",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "EqualFuelConsumptionLast_1Watch",
                table: "CarFuelConsumptions",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "EqualFuelConsumptionFirst_2Watch",
                table: "CarFuelConsumptions",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "EqualFuelConsumptionFirst_1Watch",
                table: "CarFuelConsumptions",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
