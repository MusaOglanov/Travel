using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Travel.Models
{
    public class HotelType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool İsDeactive { get; set; }
        public List<HotelCategory> HotelCategories { get; set; }

    }
}
