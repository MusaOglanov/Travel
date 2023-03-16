using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Travel.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsDomestic { get; set; }
        public string Image { get; set; }
        public int Star { get; set; }
        public int Price { get; set; }
        public string Info { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public string WebSite { get; set; }
        public bool RoomAvailable { get; set; }
        public int CheckInTime { get; set; }
        public int CheckOutTime { get; set; }
        public int Rating { get; set; }
        public List<HotelCategory> HotelCategories { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }

    }
}
