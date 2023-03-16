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
        #region Index
        public async Task<IActionResult> Index()
        {
            List<Hotel> hotels = await _db.Hotels
                .Include(h => h.HotelCategories)
                .ThenInclude(h => h.HotelType)
                .ToListAsync();
            return View(hotels);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }
        #region Get

        #endregion

        #region Post

        #endregion


        #endregion
    }
}
