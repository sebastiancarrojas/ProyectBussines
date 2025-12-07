using Microsoft.AspNetCore.Mvc;

namespace PersonalProyect.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Products()
        {
            return View();
        }
        
        public IActionResult Customers()
        {
            return View();
        }
    }
}
