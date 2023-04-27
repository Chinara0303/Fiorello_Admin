using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practice.Migrations
{
    public partial class AddSettingWithFooterrs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Key", "Value" },
                values: new object[] { "FbSocial", "footer-bottom-1.png" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Key", "Value" },
                values: new object[] { "LdSocial", "LINKEDIN" });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Key", "SoftDelete", "Value" },
                values: new object[] { 3, "FbSocial", false, "FACEBOOK" });
        }
    }
}
