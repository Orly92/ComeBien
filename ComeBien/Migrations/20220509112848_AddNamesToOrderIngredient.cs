using Microsoft.EntityFrameworkCore.Migrations;

namespace ComeBien.Migrations
{
    public partial class AddNamesToOrderIngredient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EnName",
                table: "OrderProductIngredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EsName",
                table: "OrderProductIngredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FrName",
                table: "OrderProductIngredients",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnName",
                table: "OrderProductIngredients");

            migrationBuilder.DropColumn(
                name: "EsName",
                table: "OrderProductIngredients");

            migrationBuilder.DropColumn(
                name: "FrName",
                table: "OrderProductIngredients");
        }
    }
}
