namespace Travel.Models
{
    public class HotelCategory
    {
        public int Id { get; set; }
        public Hotel Hotel { get; set; }
        public int HotelId { get; set; }
        public HotelType HotelType { get; set; }
        public int HotelTypeId { get; set; }

    }
}
