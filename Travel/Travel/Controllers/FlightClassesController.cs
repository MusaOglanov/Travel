using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Travel.DAL;
using Travel.Models;

namespace Travel.Controllers
{
    public class FlightClassesController : Controller
    {
        private readonly AppDbContext _db;
        public FlightClassesController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<FlightClass> classes = await _db.FlightClasses.ToListAsync();
            return View(classes);
        }

        #region Create

        #region get
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FlightClass flightclass)
        {
            bool IsExist = await _db.FlightClasses.AnyAsync(t => t.Name == flightclass.Name);
            if (IsExist)
            {
                ModelState.AddModelError("Name", "This name already is exist");
                return View();
            }


            await _db.FlightClasses.AddAsync(flightclass);
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
            FlightClass dbClass = await _db.FlightClasses.FirstOrDefaultAsync(f => f.Id == id);
            if (dbClass == null)
            {
                return BadRequest();
            }

            return View(dbClass);
        }
        #endregion

        #region Update

        #region get
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            FlightClass dbClass = await _db.FlightClasses.FirstOrDefaultAsync(f => f.Id == id);
            if (dbClass == null)
            {
                return BadRequest();
            }

            return View(dbClass);
        }
        #endregion

        #region post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(FlightClass flightClass , int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            FlightClass dbClass = await _db.FlightClasses.FirstOrDefaultAsync(f => f.Id == id);
            if (dbClass == null)
            {
                return BadRequest();
            }

           bool IsExist=await _db.FlightClasses.AnyAsync(f=>f.Name==flightClass.Name&&f.Id!=id);
            if (IsExist)
            {
                ModelState.AddModelError("Name", "Bu ad daha Əvvəl istifadə edilib!");
                return View (dbClass);
            }
            dbClass.Name = flightClass.Name;
            dbClass.Info = flightClass.Info;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #endregion

        #region Activity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            FlightClass dbClass = await _db.FlightClasses.FirstOrDefaultAsync(t => t.Id == id);

            if (dbClass == null)
            {
                return BadRequest();
            }

            if (dbClass.IsDeactive)
            {
                dbClass.IsDeactive = false;
            }
            else
            {
                dbClass.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
