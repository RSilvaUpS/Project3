using Microsoft.AspNetCore.Mvc;

namespace Mod3_Client.Controllers
{
    public class PendingEntrega : Controller
    {
        public async Task<IActionResult> IndexAdmin()
        {
            if (HttpContext.Session.GetString("admin") == "True")
            {
                int nif = Convert.ToInt32(HttpContext.Session.GetString("nif"));
                List<Models.PendingEntrega> pe = new List<Models.PendingEntrega>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5082/api/PendingEntrega"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        pe = JsonConvert.DeserializeObject<List<Models.PendingEntrega>>(apiResponse);
                    }
                
                }
                return View(pe);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("nif") != null)
            {
                ViewBag.Message = TempData["Message"];
                TempData["Message"] = "";
                int nif = Convert.ToInt32(HttpContext.Session.GetString("nif"));
                List<Models.PendingEntrega> pe = new List<Models.PendingEntrega>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5082/api/PendingEntrega/" + nif))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        pe = JsonConvert.DeserializeObject<List<Models.PendingEntrega>>(apiResponse);
                    }
                }
                return View(pe);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
