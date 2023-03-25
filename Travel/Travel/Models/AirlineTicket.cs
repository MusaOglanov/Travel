using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Travel.Models
{
    public class AirlineTicket
    {
        public int Id { get; set; }
        [Required]
        public string AirlineCompany { get; set; }
        public string FlightNumber { get; set; }
        public int DepartureAirportId { get; set; }
        public Airport DepartureAirport { get; set; }
        public int ArrivalAirportId { get; set; }
        public Airport ArrivalAirport { get; set; }
        public bool IsTransfer { get; set; }
        public int TransferAirportId { get; set; }
        public Airport TransferAirport { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public int TicketPrice { get; set; }
        public TimeSpan FlightDuration { get; set; }
        public string BaggageAllowance { get; set; }
        public string CarryOnAllowance { get; set; }
        public string FlightDescription { get; set; }
        public FlightCategory FlightCategory { get; set; }
        public int FlightCategoryId { get; set; }
        public SeatClass SeatClass { get; set; }
        public int SeatClassId { get; set; }
        public string FlightCode { get; set; }
        public bool HassMealService { get; set; }
        public string MealDescription { get; set; }
        public bool IsDeactive { get; set; }
    }
}
