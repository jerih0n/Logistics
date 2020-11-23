using System.Threading.Tasks;
using Logistics.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;



namespace Logistics.Controllers
{
    public class RoutsController : Controller
    {
        private readonly IDBAccess _dbAccess;

        public RoutsController(IDBAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        // GET: Routs
        public async Task<IActionResult> Index()
        {
            var model = _dbAccess.GetRoutes();
            return View(model);
        }

        // GET: Routs/Create
        public IActionResult Create()
        {
            var cities = _dbAccess.GetCities();
            ViewData["EndCityId"] = new SelectList(cities, "Id", "Name");
            ViewData["StartCityId"] = new SelectList(cities, "Id", "Name");
            return View();
        }

        // POST: Routs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartCityId,EndCityId,DateUpdated")] RoutDTO rout)
        {
            if (ModelState.IsValid)
            {
                _dbAccess.AddRout(rout.StartCityId, rout.EndCityId, rout.Distance);
                _dbAccess.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            var cities = _dbAccess.GetCities();
            ViewData["EndCityId"] = new SelectList(cities, "Id", "Name", rout.EndCityId);
            ViewData["StartCityId"] = new SelectList(cities, "Id", "Name", rout.StartCityId);
            return View(rout);
        }

        // GET: Routs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rout = _dbAccess.FindRout(id.Value);
            if (rout == null)
            {
                return NotFound();
            }
            var cities = _dbAccess.GetCities();
            ViewData["EndCityId"] = new SelectList(cities, "Name", "Id", rout.EndCityId);
            ViewData["StartCityId"] = new SelectList(cities, "Name", "Id", rout.StartCityId);
            return View(rout);
        }

        // POST: Routs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartCityId,EndCityId,DateUpdated")] RoutDTO rout)
        {
            if (id != rout.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbAccess.EditRout(rout);
                    _dbAccess.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoutExists(rout.Id))
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
            var cities = _dbAccess.GetCities();
            ViewData["EndCityId"] = new SelectList(cities, "Id", "Name", rout.EndCityId);
            ViewData["StartCityId"] = new SelectList(cities, "Id", "Name", rout.StartCityId);
            return View(rout);
        }

        // GET: Routs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rout = _dbAccess.FindRout(id.Value);
            if (rout == null)
            {
                return NotFound();
            }

            return View(rout);
        }

        // POST: Routs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _dbAccess.DeleteRout(id);
            _dbAccess.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool RoutExists(int id)
        {
            return _dbAccess.FindRout(id) != null;
        }
    }
}
