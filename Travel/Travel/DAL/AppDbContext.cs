using Microsoft.EntityFrameworkCore;
using Travel.Models;

namespace Travel.DAL
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
            
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelType> HotelTypes { get; set; }
        public DbSet<HotelCategory> HotelCategories { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<FlightCategory> FlightCategories { get; set; }



    }
}
