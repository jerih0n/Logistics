using System.Threading.Tasks;
using Logistics.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Web.Controllers
{
    public class CitiesController : Controller
    {
        private readonly IDBAccess _dbAccess;

        public CitiesController(IDBAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        // GET: Cities
        public async Task<IActionResult> Index()
        {
            return View(_dbAccess.GetCities());
        }

        // GET: Cities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DateUpdated")] CityDTO city)
        {
            if (ModelState.IsValid)
            {
                _dbAccess.AddCity(city.Name);
                _dbAccess.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = _dbAccess.FindCity(id.Value);
            if (city == null)
            {
                return NotFound();
            }
            return View(city);
        }

        // POST: Cities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DateUpdated")] CityDTO city)
        {
            if (id != city.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbAccess.EditCity(city);
                    _dbAccess.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(city.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = _dbAccess.FindCity(id.Value);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _dbAccess.DeleteCity(id);
            _dbAccess.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool CityExists(int id)
        {
            return _dbAccess.FindCity(id) != null;
        }
    }
}
