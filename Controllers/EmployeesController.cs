using ERPSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace UI.Controllers
{
    public class EmployeesController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7038/api/");
        private readonly HttpClient _client;

        public EmployeesController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
        }

        private bool SetAuthorizationHeader()
        {
            if (TempData.TryGetValue("AuthToken", out object token))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());
                return true;
            }
            return false;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (SetAuthorizationHeader())
            {
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Employees/GetEmployees").Result;
                if (response.IsSuccessStatusCode) 
                    return View(response.Content);
            }
            return View("Home/Login");
        }

        [HttpGet]
        public ActionResult Details()
        {
            if (SetAuthorizationHeader())
            {

            }

            return View("Home/Login");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            if (SetAuthorizationHeader())
            {

                return View();
            }
            return View("Home/Login");
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (SetAuthorizationHeader())
            {
                return View();
            }

            return View("Home/Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeDto employeeDto)
        {
            if (SetAuthorizationHeader())
            {
                return View();
            }
            return View("Home/Login");
        }

        public ActionResult Edit(int id)
        {
            if (SetAuthorizationHeader())
            {
                return View();
            }
            return View("Home/Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            if (SetAuthorizationHeader())
            {
                return View();
            }
            return View("Home/Login");
        }

        public ActionResult Delete(int id)
        {
            if (SetAuthorizationHeader())
            {
                return View();
            }
            return View("Home/Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            if (SetAuthorizationHeader())
            {
                return View();
            }
            return View("Home/Login");
        }
    }
}
