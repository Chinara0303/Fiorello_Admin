using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practice.Migrations
{
    public partial class AddSettingWithFooter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Key", "SoftDelete", "Value" },
                values: new object[] { 2, "LdSocial", false, "LINKEDIN" });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Key", "SoftDelete", "Value" },
                values: new object[] { 3, "FbSocial", false, "FACEBOOK" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Key", "SoftDelete", "Value" },
                values: new object[] { 1, "HeaderLogo", false, "logo.png" });
        }
    }
}
