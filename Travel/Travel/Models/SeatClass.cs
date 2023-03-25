using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Travel.Models
{
    public class SeatClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [MaxLength(500)]
        public string Info { get; set; }
        public bool IsDeactive { get; set; }
        public List<AirlineTicket> AirlineTickets { get; set; }
    }
}
