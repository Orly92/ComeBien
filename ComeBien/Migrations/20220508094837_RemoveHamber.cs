using Microsoft.EntityFrameworkCore.Migrations;

namespace ComeBien.Migrations
{
    public partial class RemoveHamber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "EnDescription", "EnName", "EsDescription", "EsName", "FrDescription", "FrName", "Price" },
                values: new object[] { 2, "Build your own burger", "Burger", "Construye tu propia hamburguesa", "Hamburguesa", "Construisez votre propre hamburger", "Burger", 4m });
        }
    }
}
