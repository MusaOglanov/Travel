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
        public int TransferPrice { get; set; }

        public bool IsReturn { get; set; }
        
        public int ReturnAirportId { get; set; }
        public Airport ReturnAirport { get; set; }
        public int ReturnPrice { get; set; }

        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public int TicketPrice { get; set; }
        public TimeSpan FlightDuration { get; set; }
        public bool HassBaggage { get; set; }
        public string BaggageAllowance { get; set; }
        public int BaggagePrice { get; set; }

        public string Handluggage { get; set; }
        public string FlightDescription { get; set; }
        public SeatClass SeatClass { get; set; }
        public int SeatClassId { get; set; }
        public bool HassMealService { get; set; }
        public string MealDescription { get; set; }
        public int MealPrice { get; set; }

        public bool IsDeactive { get; set; }
    }
}
