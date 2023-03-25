using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Travel.DAL;
using Travel.Models;

namespace Travel.Controllers
{
    public class FlightCategoriesController : Controller
    {
        private readonly AppDbContext _db;
        public FlightCategoriesController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<FlightCategory> categories = await _db.FlightCategories.ToListAsync();
            return View(categories);
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
        public async Task<IActionResult> Create(FlightCategory flightcategory)
        {
            bool IsExist = await _db.FlightCategories.AnyAsync(t => t.Name == flightcategory.Name);
            if (IsExist)
            {
                ModelState.AddModelError("Name", "This name already is exist");
                return View();
            }


            await _db.FlightCategories.AddAsync(flightcategory);
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
            FlightCategory dbCategory=await _db.FlightCategories.FirstOrDefaultAsync(f => f.Id == id);
            if(dbCategory == null)
            {
                return BadRequest();
            }

            return View(dbCategory);
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
            FlightCategory dbCategory = await _db.FlightCategories.FirstOrDefaultAsync(f => f.Id == id);
            if (dbCategory == null)
            {
                return BadRequest();
            }

            return View(dbCategory);
        }
        #endregion

        #region post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(FlightCategory flightcategory,int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            FlightCategory dbCategory = await _db.FlightCategories.FirstOrDefaultAsync(f => f.Id == id);
            if (dbCategory == null)
            {
                return BadRequest();
            }

            bool IsExist = await _db.FlightCategories.AnyAsync(t => t.Name == flightcategory.Name&&t.Id!=id);
            if (IsExist)
            {
                ModelState.AddModelError("Name", "This name already is exist");
                return View(dbCategory);
            }

            dbCategory.Name=flightcategory.Name;
            dbCategory.Info=flightcategory.Info;
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
            FlightCategory dbCategory = await _db.FlightCategories.FirstOrDefaultAsync(t => t.Id == id);

            if (dbCategory == null)
            {
                return BadRequest();
            }

            if (dbCategory.IsDeactive)
            {
                dbCategory.IsDeactive = false;
            }
            else
            {
                dbCategory.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion

    }
}
