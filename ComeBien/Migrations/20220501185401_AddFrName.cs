using Microsoft.EntityFrameworkCore.Migrations;

namespace ComeBien.Migrations
{
    public partial class AddFrName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FrDescription",
                table: "Products",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FrName",
                table: "Products",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FrName",
                table: "Ingredients",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 1,
                column: "FrName",
                value: "Fromage");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 2,
                column: "FrName",
                value: "Jambon");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 3,
                column: "FrName",
                value: "Bacon");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 4,
                column: "FrName",
                value: "Saucisse");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 5,
                column: "FrName",
                value: "Ananas");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 6,
                column: "FrName",
                value: "Oignon");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FrDescription", "FrName" },
                values: new object[] { "Construisez votre propre pizza", "Pizza" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FrDescription", "FrName" },
                values: new object[] { "Construisez votre propre hamburger", "Burger" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FrDescription",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FrName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FrName",
                table: "Ingredients");
        }
    }
}
