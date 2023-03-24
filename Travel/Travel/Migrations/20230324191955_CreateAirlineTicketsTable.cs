using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.Migrations
{
    public partial class CreateAirlineTicketsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AirlineTickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirlineCompany = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlightNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartureAirportId = table.Column<int>(type: "int", nullable: false),
                    ArrivalAirportId = table.Column<int>(type: "int", nullable: false),
                    IsTransfer = table.Column<bool>(type: "bit", nullable: false),
                    TransferAirportId = table.Column<int>(type: "int", nullable: false),
                    DepartureDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketPrice = table.Column<int>(type: "int", nullable: false),
                    FlightDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                    BaggageAllowance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarryOnAllowance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeatType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightCategoryId = table.Column<int>(type: "int", nullable: false),
                    FlightCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HassMealService = table.Column<bool>(type: "bit", nullable: false),
                    MealDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirlineTickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirlineTickets_Airports_ArrivalAirportId",
                        column: x => x.ArrivalAirportId,
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AirlineTickets_Airports_DepartureAirportId",
                        column: x => x.DepartureAirportId,
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AirlineTickets_Airports_TransferAirportId",
                        column: x => x.TransferAirportId,
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AirlineTickets_FlightCategories_FlightCategoryId",
                        column: x => x.FlightCategoryId,
                        principalTable: "FlightCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirlineTickets_ArrivalAirportId",
                table: "AirlineTickets",
                column: "ArrivalAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_AirlineTickets_DepartureAirportId",
                table: "AirlineTickets",
                column: "DepartureAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_AirlineTickets_FlightCategoryId",
                table: "AirlineTickets",
                column: "FlightCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AirlineTickets_TransferAirportId",
                table: "AirlineTickets",
                column: "TransferAirportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirlineTickets");
        }
    }
}
