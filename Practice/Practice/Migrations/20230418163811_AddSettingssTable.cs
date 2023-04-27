using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practice.Migrations
{
    public partial class AddSettingssTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Key",
                value: "CardLogo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Key",
                value: "FbSocial");
        }
    }
}
