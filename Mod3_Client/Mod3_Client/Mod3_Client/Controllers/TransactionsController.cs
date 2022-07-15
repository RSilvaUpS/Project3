using Microsoft.AspNetCore.Mvc;

namespace Mod3_Client.Controllers
{
    public class TransactionsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("nif") != null)
            {
                ViewBag.Message = TempData["Message"];
                TempData["Message"] = "";
                int nif = Convert.ToInt32(HttpContext.Session.GetString("nif"));
                List<Transactions> transactions = new List<Transactions>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5082/api/Transactions/FromNif?nif=" + nif))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        transactions = JsonConvert.DeserializeObject<List<Transactions>>(apiResponse);
                    }
                }
                return View(transactions);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public async Task<IActionResult> Return(int transactionId)
        {
            if (HttpContext.Session.GetString("nif") != null)
            {
                int nif = Convert.ToInt32(HttpContext.Session.GetString("nif"));
                string message = "";
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5082/api/Transactions/Return?transactionId=" + transactionId + "&nif=" + nif))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        message = JsonConvert.ToString(apiResponse);
                    }
                }
                TempData["Message"] = message;
                return RedirectToAction("Index", "Transactions");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public async Task<IActionResult> IndexAdmin()
        {
            if (HttpContext.Session.GetString("admin") == "True")
            {
                ViewBag.Message = TempData["Message"];
                TempData["Message"] = "";
                int nif = Convert.ToInt32(HttpContext.Session.GetString("nif"));
                List<Transactions> transactions = new List<Transactions>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5082/api/Transactions/"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        transactions = JsonConvert.DeserializeObject<List<Transactions>>(apiResponse);
                    }
                }
                return View(transactions);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public async Task<IActionResult> LendBook(int isbn, int nucleoId)
        {
            if (HttpContext.Session.GetString("nif") != null)
            {

                int nif = Convert.ToInt32(HttpContext.Session.GetString("nif"));
                string message = "";
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5082/api/Transactions/Lend?nif=" + nif + "&isbn=" + isbn + "&nucleoId=" + nucleoId))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        message = JsonConvert.ToString(apiResponse);
                    }
                }
                TempData["Message"] = message;
                return RedirectToAction("Index", "Transactions");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

    }
}
