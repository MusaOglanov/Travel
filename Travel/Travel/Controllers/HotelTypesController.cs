using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Travel.DAL;
using Travel.Models;

namespace Travel.Controllers
{
    public class HotelTypesController : Controller
    {
        private readonly AppDbContext _db;
        public HotelTypesController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<HotelType> hotelTypes = await _db.HotelTypes.ToListAsync();
            return View(hotelTypes);
        }


        public  IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HotelType hotelType)
        {
            bool IsExist = await _db.HotelTypes.AnyAsync(t => t.Name == hotelType.Name);
            if (IsExist)
            {
                ModelState.AddModelError("Name", "This name already is exist");
                return View();
            }


            await _db.HotelTypes.AddAsync(hotelType);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
