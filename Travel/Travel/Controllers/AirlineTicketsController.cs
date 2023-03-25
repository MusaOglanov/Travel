using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
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
                 .Include(a => a.SeatClass)
                 .ToListAsync();
            return View(tickets);
        }
        #endregion


        #region Create

        #region get
        public async Task<IActionResult> Create()
        {
            ViewBag.DepatureAirport = await _db.Airports.ToListAsync();
            ViewBag.ArrivalAirport = await _db.Airports.ToListAsync();
            ViewBag.TransferAirport = await _db.Airports.ToListAsync();
            ViewBag.SeatClass = await _db.SeatClasses.ToListAsync();


            return View();
        }
        #endregion

        #region post

        #endregion

        #endregion

    }
}
