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

        #region Get
        public async Task<IActionResult> Create()
        {
            ViewBag.HotelType = await _db.HotelTypes.ToListAsync();

            return View();
        }
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

        #region get
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return View();
            }
            Hotel dbHotel = await _db.Hotels
               .Include(h => h.HotelCategories)
               .ThenInclude(h => h.HotelType)
               .FirstOrDefaultAsync(x => x.Id == id);
            if (dbHotel == null)
            {
                return BadRequest();
            }
            ViewBag.HotelTypes = await _db.HotelTypes.ToListAsync();
            return View(dbHotel);
        }
        #endregion

        #region post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Hotel hotel, int? id, int[] hoteTypesId)
        {
            if (id == null)
            {
                return View();
            }
            Hotel dbHotel = await _db.Hotels
                .Include(h => h.HotelCategories)
                .ThenInclude(h => h.HotelType)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (dbHotel == null)
            {
                return BadRequest();
            }
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
            bool isExist = await _db.Hotels.AnyAsync(h => h.Name == hotel.Name && h.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This name already is exist");
            }
            if (hotel.Star < 1 || hotel.Star > 5)
            {
                ModelState.AddModelError("Star", "Please choose a number between 1 and 5 ");
                return View(dbHotel);
            }
            if (hotel.Rating < 1 || hotel.Rating > 10)
            {
                ModelState.AddModelError("Rating", "Please choose a number between 1 and 10 ");
                return View(dbHotel);
            }
            if (hotel.Photo != null)
            {
                if (!hotel.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Please slect image file");
                    return View(dbHotel);
                }
                if (hotel.Photo.IsOlder2MB())
                {
                    ModelState.AddModelError("Photo", "Max 2MB");
                    return View(dbHotel);
                }
                string folder = Path.Combine(_env.WebRootPath, "assets", "img");
                dbHotel.Image = await hotel.Photo.SaveImageAsync(folder);
            }
            dbHotel.Name = hotel.Name;
            dbHotel.IsDomestic = hotel.IsDomestic;
            dbHotel.HotelCategories = hotel.HotelCategories;
            dbHotel.Country = hotel.Country;
            dbHotel.City = hotel.City;
            dbHotel.Adress = hotel.Adress;
            dbHotel.RoomAvailable = hotel.RoomAvailable;
            dbHotel.Star = hotel.Star;
            dbHotel.Info = hotel.Info;
            dbHotel.Email = hotel.Email;
            dbHotel.PhoneNumber = hotel.PhoneNumber;
            dbHotel.WebSite = hotel.WebSite;
            dbHotel.Price = hotel.Price;


            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }


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
