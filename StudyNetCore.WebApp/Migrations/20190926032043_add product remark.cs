using Microsoft.EntityFrameworkCore.Migrations;

namespace StudyNetCore.WebApp.Migrations
{
    public partial class addproductremark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "TProducts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remark",
                table: "TProducts");
        }
    }
}
