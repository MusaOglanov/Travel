using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.Migrations
{
    public partial class AddBaggagePriceAndMealPriceColumnsToAirlineTicketsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BaggagePrice",
                table: "AirlineTickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MealPrice",
                table: "AirlineTickets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaggagePrice",
                table: "AirlineTickets");

            migrationBuilder.DropColumn(
                name: "MealPrice",
                table: "AirlineTickets");
        }
    }
}
