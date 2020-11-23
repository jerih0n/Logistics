
using System.Threading.Tasks;
using Logistics.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace Logistics.Controllers
{
    public class LogisticsCentersController : Controller
    {
        private readonly IDBAccess _dbAccess;

        public LogisticsCentersController(IDBAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        // GET: LogisticsCenters
        public async Task<IActionResult> Index()
        {
            return View(_dbAccess.GetLogisticsCenters());
        }

        // GET: LogisticsCenters/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_dbAccess.GetCities(), "Id", "Name");
            return View();
        }

        // POST: LogisticsCenters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CityId,DateUpdated")] LogisticsCenterDTO logisticsCenter)
        {
            if (ModelState.IsValid)
            {
                _dbAccess.AddLogisticsCenter(logisticsCenter);
                _dbAccess.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_dbAccess.GetCities(), "Id", "Name", logisticsCenter.CityId);
            return View(logisticsCenter);
        }

        // GET: LogisticsCenters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logisticsCenter = _dbAccess.FindLogisticsCenter(id.Value);
            if (logisticsCenter == null)
            {
                return NotFound();
            }

            return View(logisticsCenter);
        }

        // POST: LogisticsCenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _dbAccess.DeleteLogisticsCenter(id);
            _dbAccess.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool LogisticsCenterExists(int id)
        {
            return _dbAccess.FindLogisticsCenter(id) != null;
        }
    }
}
