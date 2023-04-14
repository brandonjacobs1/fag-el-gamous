using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fag_el_gamous.Controllers
{
    public class ModelController : Controller
    {
        private readonly HttpClient _client;

        public ModelController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://54.163.213.32/");
        }

        public IActionResult Model()
        {
            return View();
        }

        public IActionResult Supervised()
        {
            return View();
        }

        public IActionResult Unsupervised()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Sex(string eastwest, double depth, string haircolor, bool samplescollected, string ageatdeath, string wrapping)
        {
            // Build the JSON data to pass to the API
            var data = new
            {
                eastwest = eastwest,
                depth = depth,
                haircolor = haircolor,
                samplescollected = samplescollected,
                ageatdeath = ageatdeath,
                wrapping = wrapping
            };

            // Make the API call
            var response = await _client.PostAsJsonAsync("predict", data);

            // Get the result
            var result = await response.Content.ReadAsStringAsync();

            // Parse the JSON string to a JObject
            var jsonObject = JObject.Parse(result);

            // Extract the value of the "sex" property
            var sex = (string)jsonObject["sex"];

            // Pass the result to the view
            ViewBag.Sex = sex;

            // Render the Model view
            return View("Supervised");
        }



        [HttpPost]
        public async Task<IActionResult> Hd(double depth, double southtohead, double westtohead, double westtofeet, double southtofeet, string area, string wrapping)
        {
            // Build the JSON data to pass to the API
            var data = new
            {
                depth = depth,
                southtohead = southtohead,
                westtohead = westtohead,
                westtofeet = westtofeet,
                southtofeet = southtofeet,
                area = area,
                wrapping = wrapping
            };

            // Make the API call
            var response = await _client.PostAsJsonAsync("hd", data);

            // Get the result
            var result = await response.Content.ReadAsStringAsync();

            // Parse the JSON string to a JObject
            var jsonObject = JObject.Parse(result);

            // Extract the value of the "sex" property
            var headdirection = (string)jsonObject["headdirection"];

            // Pass the result to the view
            ViewBag.Hd = headdirection;

            // Render the Hd view
            return View("Supervised");
        }
    }
}

