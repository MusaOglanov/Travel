using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.Migrations
{
    public partial class AddIsDomesticCoulmnToHotelTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDomestic",
                table: "HotelTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDomestic",
                table: "HotelTypes");
        }
    }
}
