using Azure;
using ERPSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace UI.Controllers
{
    public class EmployeesController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7038/api/");
        private readonly HttpClient _client;

        public EmployeesController()
        {
            _client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
            _client.BaseAddress = baseAddress;
        }

        private void AddTokenHeader()
        {
            // Get the token from the session
            string? authToken = HttpContext.Session.GetString("AuthToken");

            // Add the token to the request headers
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        }

        [HttpGet]
        public IActionResult Index()
        {
            AddTokenHeader();
            var response = _client.GetAsync(_client.BaseAddress + "Employees/GetEmployees");

            var employeesJson = response.ToJson();
            var employees = JsonConvert.DeserializeObject<List<Employee>>(employeesJson);
            // Now you have the list of employees.

            return View(employees);


            //return View("../Home/Login");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            AddTokenHeader();
            var response = _client.GetAsync(_client.BaseAddress + "Employees/GetEmployee/{id}");
            var employeeJson = response.ToJson();
            var employee = JsonConvert.DeserializeObject<Employee>(employeeJson);
            return View(employee);
        }

        [HttpGet]
        public ActionResult Create()
        {
            AddTokenHeader();
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeDto employeeDto)
        {
            AddTokenHeader();

            string data = JsonConvert.SerializeObject(employeeDto);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var reponse = _client.PostAsync(_client.BaseAddress + "Employees/PostEmployee", content);

            return View();
        }

        public ActionResult Edit()
        {
            AddTokenHeader();
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, EmployeeDto employeeDto)
        {
            AddTokenHeader();

            string data = JsonConvert.SerializeObject(employeeDto);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = _client.PutAsync(_client.BaseAddress + "Employees/PutEmployee/{id}", content);
            return View();
        }

        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            AddTokenHeader();

            var reponse = _client.DeleteAsync(_client.BaseAddress + "Employees/DeleteEmployee/{id}");
            return View("Home/Login");
        }
    }
}
