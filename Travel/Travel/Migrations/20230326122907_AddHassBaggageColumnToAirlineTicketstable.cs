using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.Migrations
{
    public partial class AddHassBaggageColumnToAirlineTicketstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarryOnAllowance",
                table: "AirlineTickets",
                newName: "Handluggage");

            migrationBuilder.AddColumn<bool>(
                name: "HassBaggage",
                table: "AirlineTickets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HassBaggage",
                table: "AirlineTickets");

            migrationBuilder.RenameColumn(
                name: "Handluggage",
                table: "AirlineTickets",
                newName: "CarryOnAllowance");
        }
    }
}
