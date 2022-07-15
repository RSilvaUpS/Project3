using Microsoft.AspNetCore.Mvc;

namespace Mod3_Client.Controllers
{
    public class ImagensController : Controller
    {
        public async Task<IActionResult> Get(int isbn)
        {
            if (HttpContext.Session.GetString("nif") != null)
            {
                Imagens cover = new Imagens();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5082/api/Imagens/" + isbn))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        cover = JsonConvert.DeserializeObject<Imagens>(apiResponse);
                    }
                }
                return View(cover);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public async Task<IActionResult> EditCover(int isbn)
        {
            if (HttpContext.Session.GetString("admin") == "True")
            {
                Imagens cover = new Imagens();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5082/api/Imagens/" + isbn))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        cover = JsonConvert.DeserializeObject<Imagens>(apiResponse);
                    }
                }
                return View(cover);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditCover(Imagens cover)
        {
            if (HttpContext.Session.GetString("admin") == "True")
            {
                
                    var formFile = cover.fileUpload.FormFile;
                    Byte[] bytes = null;

                    using (MemoryStream ms = new MemoryStream())
                    {

                        formFile.CopyTo(ms);

                        bytes = ms.ToArray();
                    }
                    cover.CoverImage = bytes;

                    using (var httpClient = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(cover), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PutAsync("http://localhost:5082/api/Imagens", content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            cover = JsonConvert.DeserializeObject<Imagens>(apiResponse);
                        }
                    }
                    return View(cover);
                
                
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }


}
