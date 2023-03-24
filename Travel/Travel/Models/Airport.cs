using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Travel.Models
{
    public class Airport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        [MaxLength(3)]
        public string Code { get; set; }
        public bool IsDeactive { get; set; }
        [NotMapped]
        public List<AirlineTicket> AirlineTickets { get; set; }
    }
}
