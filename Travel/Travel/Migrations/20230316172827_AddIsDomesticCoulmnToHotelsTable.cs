using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.Migrations
{
    public partial class AddIsDomesticCoulmnToHotelsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDomestic",
                table: "Hotels",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDomestic",
                table: "Hotels");
        }
    }
}
