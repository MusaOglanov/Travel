using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Travel.Models;

namespace Travel.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelType> HotelTypes { get; set; }
        public DbSet<HotelCategory> HotelCategories { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<AirlineTicket> AirlineTickets { get; set; }
        public DbSet<SeatClass> SeatClasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AirlineTicket>()
                .HasOne(t => t.DepartureAirport)
                .WithMany()
                .HasForeignKey(t => t.DepartureAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AirlineTicket>()
                .HasOne(t => t.ArrivalAirport)
                .WithMany()
                .HasForeignKey(t => t.ArrivalAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AirlineTicket>()
               .HasOne(t => t.TransferAirport)
               .WithMany()
               .HasForeignKey(t => t.TransferAirportId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AirlineTicket>()
                .HasOne(t => t.ReturnAirport)
                .WithMany()
                .HasForeignKey(t => t.ReturnAirportId)
                .OnDelete(DeleteBehavior.Restrict);
            


        }

    }


    
}
