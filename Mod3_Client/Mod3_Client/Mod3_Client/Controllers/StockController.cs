using Microsoft.AspNetCore.Mvc;

namespace Mod3_Client.Controllers
{
    public class StockController : Controller
    {
        public async Task<IActionResult> Index(int isbn)
        {
            if (HttpContext.Session.GetString("nif") != null)
            {
                List<Stock> s = new List<Stock>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5082/api/Stock/StockBook?isbn=" + isbn))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        s = JsonConvert.DeserializeObject<List<Stock>>(apiResponse);
                    }
                }
                return View(s);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult Transfer()
        {
            if (HttpContext.Session.GetString("admin") == "True")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    

        [HttpPost]
        public async Task<IActionResult> Transfer(int isbn, int trasnferStock, int nucleoIn, int nucleoOut)
        {
            if (HttpContext.Session.GetString("admin") == "True")
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject("string"), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync("http://localhost:5082/api/Stock/transfer?isbn=" + isbn + "&transferStock=" + trasnferStock + "&nucleoIn=" + nucleoIn+ "&nucleoOut=" + nucleoOut, null))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }

                }
                return RedirectToAction("Index", new {isbn = isbn});
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult Update(int isbn)
        {
        if (HttpContext.Session.GetString("admin") == "True")
        {
            return View();
        }
        else
        {
            return RedirectToAction("Index", "Login");
        }
    }

        [HttpPost]
        public async Task<IActionResult> Update(Stock stock)
        {
            if (HttpContext.Session.GetString("admin") == "True")
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(stock), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync("http://localhost:5082/api/Stock/update", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }

                }
                return View(stock);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
