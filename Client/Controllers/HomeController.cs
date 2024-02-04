using Client.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<ActionResult> Edit(int Id)
        {
            PhoneNumberDto phoneNumberDto = new PhoneNumberDto();   

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://telecom.azurewebsites.net/api/Telecom/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("https://telecom.azurewebsites.net/api/Telecom/" + Id);
                string jsondata = await response.Content.ReadAsStringAsync();
                jsondata = jsondata.Substring(1, jsondata.Length - 2);
                phoneNumberDto = System.Text.Json.JsonSerializer.Deserialize<PhoneNumberDto>(jsondata);
            }
            return View(phoneNumberDto);
        }

        [HttpPost]
        public ActionResult Edit(PhoneNumberDto phoneNumberDto)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonObject = JsonSerializer.Serialize(phoneNumberDto);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri("https://telecom.azurewebsites.net/");
                var response = client.PutAsync("api/Telecom/" + phoneNumberDto.id, content).Result;

                if (response.IsSuccessStatusCode) Console.Write("Success");
                else Console.Write("Error");
            }

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}