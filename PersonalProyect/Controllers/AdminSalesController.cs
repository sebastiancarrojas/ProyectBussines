using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PersonalProyect.Controllers
{
    public class AdminSalesController : Controller
    {
        // GET: AdminSalesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AdminSalesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminSalesController/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: AdminSalesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminSalesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminSalesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminSalesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminSalesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
