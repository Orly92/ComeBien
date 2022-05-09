using Microsoft.EntityFrameworkCore.Migrations;

namespace ComeBien.Migrations
{
    public partial class AddItemId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProductIngredients",
                table: "OrderProductIngredients");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderProductIngredients");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OrderProducts",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderProductIngredients",
                newName: "ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProductIngredients",
                table: "OrderProductIngredients",
                columns: new[] { "ItemId", "IngredientId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProductIngredients",
                table: "OrderProductIngredients");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "OrderProducts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "OrderProductIngredients",
                newName: "ProductId");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "OrderProductIngredients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProductIngredients",
                table: "OrderProductIngredients",
                columns: new[] { "OrderId", "ProductId", "IngredientId" });
        }
    }
}
