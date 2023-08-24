using ERPSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace UI.Controllers
{
    public class EmployeesController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7038/api");
        private readonly HttpClient _client;
        //private int PageSize = 4;

        public EmployeesController()
        {
            _client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
            _client.BaseAddress = baseAddress;
        }

        private void AddTokenHeader()
        {
            // Get the token from the session
            string? authToken = HttpContext.Session.GetString("AuthToken");
            // clear the header 
            _client.DefaultRequestHeaders.Clear();
            // Add the token to the request headers
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + authToken);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            AddTokenHeader();

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            List<EmployeeDto> employees = new();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/Employees/GetEmployees").Result;

            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                employees = JsonConvert.DeserializeObject<List<EmployeeDto>>(data) ?? new();
            }
            
            return View(employees);
        }


        // Now you have the list of employees

        //var employeesListViewModel = new EmployeesListViewModel()
        //{
        //    Employees = employees?.OrderBy(e => e.AccountId)
        //                    .Skip((EmployeePage - 1) * PageSize)
        //                    .Take(PageSize)
        //};
        //var pagingInfo = new PagingInfo
        //{
        //    CurrentPage = EmployeePage,
        //    ItemsPerPage = PageSize,
        //    TotalItems = employees.Count()
        //};

        //return View(employees);
        //return View("../Home/Login");


        [HttpGet]
        public IActionResult Details(int id)
        {
            AddTokenHeader();

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            var employee = new EmployeeDto();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Employees/Get/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<EmployeeDto>(data) ?? new(); 
            }

            return View(employee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            AddTokenHeader();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeDto employeeDto)
        {
            AddTokenHeader();

            string data = JsonConvert.SerializeObject(employeeDto);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            _client.PostAsync(_client.BaseAddress + "Employees/Post", content);

            return View();
        }

        [HttpGet]   
        public IActionResult Edit()
        {
            AddTokenHeader();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EmployeeDto employeeDto)
        {
            AddTokenHeader();

            string data = JsonConvert.SerializeObject(employeeDto);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            await _client.PutAsync("/Employees/Put/{id}", content);

            return RedirectToAction("Index"); // Redirect regardless of response
        }


        [HttpGet]
        public IActionResult Delete()
        {
            AddTokenHeader();
            return View();
        }

        [HttpPost]
        public IActionResult DelZete(int id)
        {
            AddTokenHeader();

             _client.DeleteAsync(_client.BaseAddress + "Employees/Delete/{id}");
           
            return View();
        }
    }
}
