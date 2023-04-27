using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practice.Migrations
{
    public partial class AddSettingWithHeaderAndFooter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Key", "SoftDelete", "Value" },
                values: new object[] { 1, "HeaderLogo", false, "logo.png" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
