using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Facebook()
        {
            var uri = string.Format(
                "https://www.facebook.com/dialog/oauth?client_id={0}&redirect_uri={1}&scope={2}",
                "1034374253265538",
                "http://localhost:7788/Auth/FacebookCode/",
                "public_profile");

            return Redirect(uri);
        }

        public IActionResult FacebookCode(string code)
        {
            var uri = string.Format(
                  "https://graph.facebook.com/v2.5/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}",
                   "1034374253265538",
                   "http://localhost:7788/Auth/FacebookCode/",  // !!!
                   "0d5ba10073168902817f1be6a477b12a",
                   code);

            var httpClient = new HttpClient();
            var response = httpClient.GetAsync(uri);
            var res = response.Result;
            if(res.IsSuccessStatusCode)
            {
                var bytes = res.Content.ReadAsByteArrayAsync().Result;
                var str = Encoding.UTF8.GetString(bytes);
                // распарсить и вернуть токен
            }
            else
            {
                // ошибка
            }

            return PartialView("AuthResultPartial");
        }
    }
}
