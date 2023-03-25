using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.Migrations
{
    public partial class CreateFlightClassesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Airports",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FlightCategoryId1",
                table: "AirlineTickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FlightClassId",
                table: "AirlineTickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GetFlightClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetFlightClasses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirlineTickets_FlightCategoryId1",
                table: "AirlineTickets",
                column: "FlightCategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_AirlineTickets_FlightClassId",
                table: "AirlineTickets",
                column: "FlightClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_AirlineTickets_FlightCategories_FlightCategoryId1",
                table: "AirlineTickets",
                column: "FlightCategoryId1",
                principalTable: "FlightCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AirlineTickets_GetFlightClasses_FlightClassId",
                table: "AirlineTickets",
                column: "FlightClassId",
                principalTable: "GetFlightClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirlineTickets_FlightCategories_FlightCategoryId1",
                table: "AirlineTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_AirlineTickets_GetFlightClasses_FlightClassId",
                table: "AirlineTickets");

            migrationBuilder.DropTable(
                name: "GetFlightClasses");

            migrationBuilder.DropIndex(
                name: "IX_AirlineTickets_FlightCategoryId1",
                table: "AirlineTickets");

            migrationBuilder.DropIndex(
                name: "IX_AirlineTickets_FlightClassId",
                table: "AirlineTickets");

            migrationBuilder.DropColumn(
                name: "FlightCategoryId1",
                table: "AirlineTickets");

            migrationBuilder.DropColumn(
                name: "FlightClassId",
                table: "AirlineTickets");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Airports",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true);
        }
    }
}
