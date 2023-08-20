using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class LinesOfBusienssController : Controller
    {
        // GET: LinesOfBusienssController
        public ActionResult Index()
        {
            return View();
        }

        // GET: LinesOfBusienssController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LinesOfBusienssController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LinesOfBusienssController/Create
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

        // GET: LinesOfBusienssController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LinesOfBusienssController/Edit/5
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

        // GET: LinesOfBusienssController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LinesOfBusienssController/Delete/5
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
