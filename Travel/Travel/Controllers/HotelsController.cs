using AllUp3.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Travel.DAL;
using Travel.Models;

namespace Travel.Controllers
{
    public class HotelsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public HotelsController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
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
        public async Task<IActionResult> Create()
        {
            ViewBag.HotelType = await _db.HotelTypes.ToListAsync();

            return View();
        }
        #region Get

        #endregion

        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Hotel hotel, int[] hoteTypesId)
        {
            ViewBag.HotelType = await _db.HotelTypes.ToListAsync();

            List<HotelCategory> hotelCategories = new List<HotelCategory>();

            foreach (var hotelTypeId in hoteTypesId)
            {
                HotelCategory hotelCategory = new HotelCategory
                {
                    HotelTypeId = hotelTypeId,
                };
                hotelCategories.Add(hotelCategory);
            }
            hotel.HotelCategories = hotelCategories;
            bool IsExist = await _db.Hotels.AnyAsync(h => h.Name == hotel.Name);
            if (IsExist)
            {
                ModelState.AddModelError("Name", "This already is exist");
                return View();

            }
            if (hotel.Star < 1 || hotel.Star > 5)
            {
                ModelState.AddModelError("Star", "Please choose a number between 1 and 5 ");
                return View();
            }
            if (hotel.Rating < 1 || hotel.Rating > 10)
            {
                ModelState.AddModelError("Rating", "Please choose a number between 1 and 10 ");
                return View();
            }
            if (hotel.Photo == null)
            {
                ModelState.AddModelError("Photo", "Please slect image ");
                return View();
            }
            if (!hotel.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Please slect image file");
                return View();
            }
            if (hotel.Photo.IsOlder2MB())
            {
                ModelState.AddModelError("Photo", "Max 2MB");
                return View();
            }
            string folder = Path.Combine(_env.WebRootPath, "assets", "img");
            hotel.Image = await hotel.Photo.SaveImageAsync(folder);


            await _db.Hotels.AddAsync(hotel);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");

        }
        #endregion


        #endregion

        #region Update
        public IActionResult Update(Hotel hotel)
        {
            return View();
        }
        #region get

        #endregion

        #region post

        #endregion

        #endregion

        #region Detail

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Hotel hotel = await _db.Hotels.FirstOrDefaultAsync(h => h.Id == id);
            if (hotel == null)
            {
                return BadRequest();
            }
            return View(hotel);
        }


        #endregion


        #region Activity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Hotel dbHotel = await _db.Hotels.Include(h => h.HotelCategories)
                .ThenInclude(h => h.HotelType).FirstOrDefaultAsync(t => t.Id == id);

            if (dbHotel == null)
            {
                return BadRequest();
            }

            if (dbHotel.İsDeactive)
            {
                dbHotel.İsDeactive = false;
            }
            else
            {
                dbHotel.İsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

    }
}
