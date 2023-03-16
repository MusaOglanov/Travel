using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.Migrations
{
    public partial class AddWebSiteCoulmnToHotelsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "WebSite",
                table: "Hotels",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WebSite",
                table: "Hotels");
        }
    }
}
