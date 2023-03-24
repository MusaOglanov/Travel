using System.Collections.Generic;

namespace Travel.Models
{
    public class FlightCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public bool IsDeactive { get; set; }
        public List<AirlineTicket> AirlineTickets { get; set; }
    }
}
