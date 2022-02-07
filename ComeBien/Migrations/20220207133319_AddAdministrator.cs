using Microsoft.EntityFrameworkCore.Migrations;

namespace ComeBien.Migrations
{
    public partial class AddAdministrator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrators",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrators", x => x.UserName);
                });

            migrationBuilder.InsertData(
                table: "Administrators",
                columns: new[] { "UserName", "Password" },
                values: new object[] { "admin", "admin1234*" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrators");
        }
    }
}
