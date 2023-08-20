using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Packaging.Signing;
using NuGet.Protocol;
using System.Diagnostics;
using System.Net.Http.Headers;
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
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }


        public IActionResult Index()
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
                    TempData["AuthToken"] = token;
                    return View("Index");
                }
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Rgister()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(LoginViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "Admins/Register",content).Result;
            if (response.IsSuccessStatusCode)
            {
                return View("Home/Index");
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