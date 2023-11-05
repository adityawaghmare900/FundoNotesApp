using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository_Layer.Migrations
{
    public partial class modifytables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "labelName",
                table: "Labels");

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "Labels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Label",
                table: "Labels");

            migrationBuilder.AddColumn<string>(
                name: "labelName",
                table: "Labels",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
