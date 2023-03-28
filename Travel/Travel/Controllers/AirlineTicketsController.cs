using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using Travel.DAL;
using Travel.Models;

namespace Travel.Controllers
{
    public class AirlineTicketsController : Controller
    {
        private readonly AppDbContext _db;
        public AirlineTicketsController(AppDbContext db)
        {
            _db = db;
        }
        #region Update
        public async Task<IActionResult> Index()
        {
            List<AirlineTicket> tickets = await _db.AirlineTickets
                 .Include(a => a.DepartureAirport)
                 .Include(a => a.ArrivalAirport)
                 .Include(a => a.TransferAirport)
                 .Include(a => a.ReturnAirport)
                 .Include(a => a.SeatClass)
                 .ToListAsync();
            return View(tickets);
        }
        #endregion


        #region Create

        #region get
        public async Task<IActionResult> Create()
        {
            ViewBag.DepartureAirport = await _db.Airports.ToListAsync();
            ViewBag.ArrivalAirport = await _db.Airports.ToListAsync();
            ViewBag.TransferAirport = await _db.Airports.ToListAsync();
            ViewBag.ReturnAirport = await _db.Airports.ToListAsync();
            ViewBag.SeatClass = await _db.SeatClasses.ToListAsync();


            return View();
        }
        #endregion

        #region post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AirlineTicket ticket, int depAirId, int arrAirId, int reAirId, int transAirId, int seatClassId,string departureDate, string departureTime, string flightDuration, string arrivalDate, string arrivalTime)
        {
            ViewBag.DepartureAirport = await _db.Airports.ToListAsync();
            ViewBag.ArrivalAirport = await _db.Airports.ToListAsync();
            ViewBag.TransferAirport = await _db.Airports.ToListAsync();
            ViewBag.ReturnAirport = await _db.Airports.ToListAsync();
            ViewBag.SeatClass = await _db.SeatClasses.ToListAsync();

            if (arrAirId == depAirId)
            {
                ModelState.AddModelError("ArrivalAirport", "Gediş və Eniş Aeroportları eyni ola bilməz");
                return View();
            }
            if (depAirId == transAirId || arrAirId == transAirId)
            {
                ModelState.AddModelError("TransferAirport", "Aeroportlar eyni ola bilməz");
                return View();
            }
            if(depAirId == reAirId || transAirId == reAirId)
            {
                ModelState.AddModelError("ReturnAirport", "Aeroportlar eyni ola bilməz");
                return View();
            }

            string departureDateTimeStr = $"{departureDate} {departureTime}";
            string arrivalDateTimeStr = $"{arrivalDate} {arrivalTime}";

            DateTime departureDateTime = DateTime.Parse(departureDateTimeStr);
            ticket.DepartureDateTime = departureDateTime;

            TimeSpan flightDurationTime = TimeSpan.Parse(flightDuration);
            ticket.FlightDuration = flightDurationTime;

            DateTime arrivalDateTime = DateTime.Parse(arrivalDateTimeStr);
            ticket.ArrivalDateTime = arrivalDateTime;

            ticket.TicketPrice += ticket.ReturnPrice + ticket.TransferPrice + ticket.BaggagePrice + ticket.MealPrice;



            ticket.DepartureAirportId = depAirId;
            ticket.ArrivalAirportId = arrAirId;
            ticket.ReturnAirportId = reAirId;
            ticket.TransferAirportId = transAirId;
            ticket.SeatClassId = seatClassId;
            await _db.AirlineTickets.AddAsync(ticket);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        #endregion

        #endregion

    }
}
