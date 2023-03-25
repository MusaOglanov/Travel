using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Travel.DAL;
using Travel.Models;

namespace Travel.Controllers
{
    public class SeatClassesController : Controller
    {
        private readonly AppDbContext _db;
        public SeatClassesController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<SeatClass> classes = await _db.SeatClasses.ToListAsync();
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
        public async Task<IActionResult> Create(SeatClass seatClass)
        {
            bool IsExist = await _db.SeatClasses.AnyAsync(t => t.Name == seatClass.Name);
            if (IsExist)
            {
                ModelState.AddModelError("Name", "This name already is exist");
                return View();
            }


            await _db.SeatClasses.AddAsync(seatClass);
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
            SeatClass dbSeatClass = await _db.SeatClasses.FirstOrDefaultAsync(f => f.Id == id);
            if (dbSeatClass == null)
            {
                return BadRequest();
            }

            return View(dbSeatClass);
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
            SeatClass dbSeatClass = await _db.SeatClasses.FirstOrDefaultAsync(f => f.Id == id);
            if (dbSeatClass == null)
            {
                return BadRequest();
            }

            return View(dbSeatClass);
        }
        #endregion

        #region post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SeatClass seatClass , int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            SeatClass dbSeatClass = await _db.SeatClasses.FirstOrDefaultAsync(f => f.Id == id);
            if (dbSeatClass == null)
            {
                return BadRequest();
            }

           bool IsExist=await _db.SeatClasses.AnyAsync(f=>f.Name== dbSeatClass.Name&&f.Id!=id);
            if (IsExist)
            {
                ModelState.AddModelError("Name", "Bu ad daha Əvvəl istifadə edilib!");
                return View (dbSeatClass);
            }
            dbSeatClass.Name = seatClass.Name;
            dbSeatClass.Info = seatClass.Info;
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
            SeatClass dbSeatClass = await _db.SeatClasses.FirstOrDefaultAsync(t => t.Id == id);

            if (dbSeatClass == null)
            {
                return BadRequest();
            }

            if (dbSeatClass.IsDeactive)
            {
                dbSeatClass.IsDeactive = false;
            }
            else
            {
                dbSeatClass.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
