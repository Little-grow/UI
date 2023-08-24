using ERPSystem.Models;
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


        public AccountsController()
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
        public IActionResult Index()
        {
            AddTokenHeader();

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            List<Account> accounts = new();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/Accounts/Get").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                accounts = JsonConvert.DeserializeObject<List<Account>>(data) ?? new();
            }

            return View(accounts);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            AddTokenHeader();

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var account = new Account();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Accounts/Get/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                account = JsonConvert.DeserializeObject<Account>(data) ?? new();
            }

            return View(account);
        }

        [HttpGet]
        public IActionResult Create()
        {
            AddTokenHeader();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Account account)
        {
            AddTokenHeader();

            string data = JsonConvert.SerializeObject(account);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            _client.PostAsync(_client.BaseAddress + "/Accounts/Post", content);

            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            AddTokenHeader();
            return View();
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] Account account)
        {
            AddTokenHeader();

            string data = JsonConvert.SerializeObject(account);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            _client.PutAsync(_client.BaseAddress + $"/Accounts/Put/{id}", content);

            return View();
        }

        [HttpGet]
        public IActionResult Delete()
        {
            AddTokenHeader();
            return View();
        }

        [HttpDelete]
        public IActionResult Delete(int id, [FromBody] Account account)
        {
            AddTokenHeader();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _client.DeleteAsync(_client.BaseAddress + "Accounts/Delete/${id}");

            return View();
        }
    }
}
