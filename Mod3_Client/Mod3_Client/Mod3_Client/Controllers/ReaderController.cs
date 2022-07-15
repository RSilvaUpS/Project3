using Microsoft.AspNetCore.Mvc;

namespace Mod3_Client.Controllers
{
    public class ReaderController : Controller
    {
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("nif") != null && HttpContext.Session.GetString("admin") =="True")
            {
                ViewBag.Message = TempData["Message"];
                TempData["Message"] = "";
                List<Reader> users = new List<Reader>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5082/api/Reader"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        users = JsonConvert.DeserializeObject<List<Reader>>(apiResponse);
                    }
                }
                return View(users);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public async Task<IActionResult> Get(int nif)
        {
            if (HttpContext.Session.GetString("nif") == nif.ToString() || HttpContext.Session.GetString("admin") == "True")
            {
                Reader user = new Reader();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5082/api/Reader/" + nif))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<Reader>(apiResponse);
                    }
                }
                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Get(Reader user)
        {
            if (HttpContext.Session.GetString("nif") == user.NIF.ToString() || HttpContext.Session.GetString("admin") == "True")
            {

                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync("http://localhost:5082/api/Reader", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<Reader>(apiResponse);
                    }
                }
                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }


        public async Task<IActionResult> Delete(int nif)
        {
            if (HttpContext.Session.GetString("admin") == "True" || HttpContext.Session.GetString("nif") == nif.ToString())
            {
                string message = "";
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync("http://localhost:5082/api/Reader/" + nif))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        message = JsonConvert.ToString(apiResponse);
                    }
                }
                TempData["Message"] = message;
                return RedirectToAction("Index","Reader");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public async Task<IActionResult> Inactive()
        {
            if (HttpContext.Session.GetString("nif") != null && HttpContext.Session.GetString("admin") == "True")
            {
                ViewBag.Message = TempData["Message"];
                TempData["Message"] = "";
                List<int> users = new List<int>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5082/api/Reader/Inactive"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        users = JsonConvert.DeserializeObject<List<int>>(apiResponse);
                    }
                }
                return View(users);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
