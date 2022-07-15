using Microsoft.AspNetCore.Mvc;

namespace Mod3_Client.Controllers
{
    public class StatsController : Controller
    {
        public async Task<IActionResult> TopNucleo()
        {
            if (HttpContext.Session.GetString("nif") != null || HttpContext.Session.GetString("admin") == "True")
            {
                Stats stats = new Stats();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5082/api/Stats/TopNucleo"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        stats = JsonConvert.DeserializeObject<Stats>(apiResponse);
                    }
                }
                return View(stats);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public async Task<IActionResult> WorstNucleo()
        {
            if (HttpContext.Session.GetString("nif") != null || HttpContext.Session.GetString("admin") == "True")
            {
                Stats stats = new Stats();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5082/api/Stats/WorstNucleo"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        stats = JsonConvert.DeserializeObject<Stats>(apiResponse);
                    }
                }
                return View(stats);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public async Task<IActionResult> TopBook()
        {
            if (HttpContext.Session.GetString("nif") != null || HttpContext.Session.GetString("admin") == "True")
            {
                Stats stats = new Stats();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5082/api/Stats/TopBook"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        stats = JsonConvert.DeserializeObject<Stats>(apiResponse);
                    }
                }
                return View(stats);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public async Task<IActionResult> TopGenre()
        {
            if (HttpContext.Session.GetString("nif") != null || HttpContext.Session.GetString("admin") == "True")
            {
                Stats stats = new Stats();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5082/api/Stats/TopGenre"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        stats = JsonConvert.DeserializeObject<Stats>(apiResponse);
                    }
                }
                return View(stats);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public async Task<IActionResult> WorstGenre()
        {
            if (HttpContext.Session.GetString("nif") != null || HttpContext.Session.GetString("admin") == "True")
            {
                Stats stats = new Stats();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5082/api/Stats/WorstGenre"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        stats = JsonConvert.DeserializeObject<Stats>(apiResponse);
                    }
                }
                return View(stats);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public async Task<IActionResult> TopAuthor()
        {
            if (HttpContext.Session.GetString("nif") != null || HttpContext.Session.GetString("admin") == "True")
            {
                Stats stats = new Stats();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5082/api/Stats/TopAuthor"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        stats = JsonConvert.DeserializeObject<Stats>(apiResponse);
                    }
                }
                return View(stats);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
