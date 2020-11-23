using System.Diagnostics;
using Logistics.Common.Models;
using Logistics.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDBAccess _dbAccess;

        public HomeController(IDBAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        public void Update()
        {
            if (ModelState.IsValid)
            {
                _dbAccess.UpdateLogistics();
                _dbAccess.SaveChanges();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
