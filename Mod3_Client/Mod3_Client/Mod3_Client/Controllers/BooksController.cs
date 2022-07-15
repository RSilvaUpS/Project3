using Microsoft.AspNetCore.Mvc;

namespace Mod3_Client.Controllers
{
    public class BooksController : Controller
    {
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("nif") != null)
            {
                ViewBag.Message = TempData["Message"];
                TempData["Message"] = "";
                int nif = Convert.ToInt32(HttpContext.Session.GetString("nif"));
                List<Books> books = new List<Books>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5082/api/Books"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        books = JsonConvert.DeserializeObject<List<Books>>(apiResponse);
                    }
                }
                return View(books);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(string search)
        {
            if (HttpContext.Session.GetString("nif") != null)
            {
                ViewBag.Message = TempData["Message"];
                TempData["Message"] = "";
                int nif = Convert.ToInt32(HttpContext.Session.GetString("nif"));
                List<Books> books = new List<Books>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5082/api/Books/Filtered?search=" + search))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        books = JsonConvert.DeserializeObject<List<Books>>(apiResponse);
                    }
                }
                return View(books);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public async Task<IActionResult> Edit(int isbn)
        {
            if (HttpContext.Session.GetString("admin") == "True")
            {
                Books books = new Books();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5082/api/Books/" + isbn))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        books = JsonConvert.DeserializeObject<Books>(apiResponse);
                    }
                }
                return View(books);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Books book)
        {
            if (HttpContext.Session.GetString("admin") == "True")
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync("http://localhost:5082/api/Books", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        book = JsonConvert.DeserializeObject<Books>(apiResponse);
                    }
                }
                return View(book);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public async Task<IActionResult> Get(int isbn)
        {
            if (HttpContext.Session.GetString("nif") != null)
            {
                Books books = new Books();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5082/api/Books/" + isbn))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        books = JsonConvert.DeserializeObject<Books>(apiResponse);
                    }
                }
                return View(books);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public async Task<IActionResult> Create(Books book)
        {
            if (HttpContext.Session.GetString("admin") == "True")
            {
                if (ModelState.IsValid)
                {
                    using (var httpClient = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PostAsync("http://localhost:5082/api/Books/NewBook", content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            book = JsonConvert.DeserializeObject<Books>(apiResponse);
                        }
                    }
                    return View(book);
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int isbn)
        {
            if (HttpContext.Session.GetString("admin") == "True")
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync("http://localhost:5082/api/Books/" + isbn))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
