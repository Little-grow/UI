using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using UI.Models;


namespace UI.Controllers
{
    public class HomeController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7038/api/");
        private readonly HttpClient _client;

        public HomeController()
        {
            _client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "Admins/Login", content).Result;

            if (response.IsSuccessStatusCode)
            {
                string responseContent = response.Content.ReadAsStringAsync().Result;

                // Deserialize the response content to extract the token
                var tokenResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);

                if (tokenResponse.TryGetValue("token", out string token))
                {
                    HttpContext.Session.SetString("AuthToken", token);

                    return View("Index"); // Redirect to home page after successful login
                }
            }
            return RedirectToAction("Login"); // Redirect to login page if login fails
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(LoginViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "Admins/Register", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return View("Login");
            }
            return View();
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            TempData.Remove("JsonToken");
            return View();
        }
    }
}