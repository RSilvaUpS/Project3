using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mod3_Client.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            TempData["Message"] = "";
            if (HttpContext.Session.GetString("nif") != null)
            {
                return RedirectToAction("index", "home");
            }
                return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexAsync(int nif, string password)
        {
            Reader reader = new Reader();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5082/api/Reader/" + nif))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reader = JsonConvert.DeserializeObject<Reader>(apiResponse);
                    if(reader == null)
                    {
                        ViewBag.Error = "User not registered";
                        return View();
                    } 
                }
                {
                }
                if (reader.Password == password)
                {
                    HttpContext.Session.SetString("nif", nif.ToString());
                    HttpContext.Session.SetString("admin", reader.IsAdmin.ToString());
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Password doesn't match user";
                    return View();
                }
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> RegisterAsync(int nif, string nome, string apelido, string password)
        {
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(apelido) || string.IsNullOrEmpty(password)){
                ViewBag.Error = "Campos não podem estar vazios";
                return View();
            }
            if (nif < 99999999 || nif > 1000000000)
            {
                ViewBag.Error = "NIF tem de ter 9 digitos";
                return View();
            }
            string message = "";
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5082/api/Reader/newUser?nif=" + nif + "&fname=" + nome + "&lname=" + apelido + "&pwd=" + password))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    message = JsonConvert.ToString(apiResponse);
                }
            }
            TempData["Message"] = message;
            return RedirectToAction("Index", "Login");
        }
    }
}
