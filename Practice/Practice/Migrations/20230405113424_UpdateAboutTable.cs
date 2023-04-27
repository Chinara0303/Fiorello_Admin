using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practice.Migrations
{
    public partial class UpdateAboutTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ListDesc",
                table: "Abouts",
                newName: "ListDesc3");

            migrationBuilder.AddColumn<string>(
                name: "ListDesc1",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ListDesc2",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListDesc1",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "ListDesc2",
                table: "Abouts");

            migrationBuilder.RenameColumn(
                name: "ListDesc3",
                table: "Abouts",
                newName: "ListDesc");
        }
    }
}
