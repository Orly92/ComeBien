using Microsoft.EntityFrameworkCore.Migrations;

namespace ComeBien.Migrations
{
    public partial class AddProductInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "EnDescription", "EnName", "EsDescription", "EsName", "Price" },
                values: new object[] { 1, "Build your own pizza", "Pizza", "Construye tu propia pizza", "Pizza", 6m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "EnDescription", "EnName", "EsDescription", "EsName", "Price" },
                values: new object[] { 2, "Build your own burger", "Burger", "Construye tu propia hamburguesa", "Hamburguesa", 4m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
