using ERPSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace UI.Controllers
{
    public class AccountsController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7038/api");
        private readonly HttpClient _client;

        private void AddTokenHeader()
        {
            // Get the token from the session
            string? authToken = HttpContext.Session.GetString("AuthToken");
            // clear the header 
            _client.DefaultRequestHeaders.Clear();
            // Add the token to the request headers
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + authToken);
        }
       
        public AccountsController(IHttpClientFactory httpClientFactory)
        {
            _client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
            _client.BaseAddress = baseAddress;
        }
        // GET: AccountsController
        [HttpGet]
        public IActionResult Index()
        {
            var authToken = HttpContext.Session.GetString("AuthToken");
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + authToken);

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            List<AccountDto> accounts = new();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/Accounts/Get").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                accounts = JsonConvert.DeserializeObject<List<AccountDto>>(data) ?? new();
            }
            return View(accounts);
        }

        // GET: AccountsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccountsController/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: AccountsController/Create
        [HttpPost]
        public ActionResult Create(AccountDto account)
        {
            AddTokenHeader();
            string data = JsonConvert.SerializeObject(account);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Accounts/Post", content).Result;
            
            return View();
        }

        // GET: AccountsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountsController/Edit/5
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

        // GET: AccountsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountsController/Delete/5
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
