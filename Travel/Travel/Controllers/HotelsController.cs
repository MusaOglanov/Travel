using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Travel.DAL;
using Travel.Models;

namespace Travel.Controllers
{
    public class HotelsController : Controller
    {
        private readonly AppDbContext _db;
        public HotelsController(AppDbContext db)
        {
            _db = db;
        }
        public async Task< IActionResult> Index()
        {
            List<Hotel> hotels = await _db.Hotels.Include(h=>h.HotelCategories).ThenInclude(h=>h.HotelType).ToListAsync();
            return View(hotels);
        }
    }
}
