using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.Migrations
{
    public partial class CreateAirlineTicketsAndAirportsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeatClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Info = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatClasses", x => x.Id);
                });

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
                    IsReturn = table.Column<bool>(type: "bit", nullable: false),
                    ReturnAirportId = table.Column<int>(type: "int", nullable: false),
                    DepartureDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketPrice = table.Column<int>(type: "int", nullable: false),
                    FlightDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                    BaggageAllowance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarryOnAllowance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeatClassId = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_AirlineTickets_Airports_ReturnAirportId",
                        column: x => x.ReturnAirportId,
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
                        name: "FK_AirlineTickets_SeatClasses_SeatClassId",
                        column: x => x.SeatClassId,
                        principalTable: "SeatClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_AirlineTickets_ReturnAirportId",
                table: "AirlineTickets",
                column: "ReturnAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_AirlineTickets_SeatClassId",
                table: "AirlineTickets",
                column: "SeatClassId");

            migrationBuilder.CreateIndex(
                name: "IX_AirlineTickets_TransferAirportId",
                table: "AirlineTickets",
                column: "TransferAirportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirlineTickets");

            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.DropTable(
                name: "SeatClasses");
        }
    }
}
