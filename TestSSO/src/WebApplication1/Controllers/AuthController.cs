using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApplication1.Models;
using Auth;
using Microsoft.AspNet.Http;

namespace WebApplication1.Controllers
{
    public class AuthController : Controller
    {
        public class FBAuthResult
        {
            public string Token { get; set; }
        }

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
            var authResult = new AuthResult();

            var tokenUri = string.Format(
                  "https://graph.facebook.com/v2.5/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}",
                   "1034374253265538",
                   "http://localhost:7788/Auth/FacebookCode/",  // !!!
                   "0d5ba10073168902817f1be6a477b12a",
                   code);

            var jToken = this.GetJson(tokenUri);
            if(jToken != null)
            {
                string accessToken = jToken.access_token;
                var meUri = string.Format(
                    "https://graph.facebook.com/me?access_token={0}",
                    accessToken);

                var jMe = this.GetJson(meUri);
                if(jMe != null)
                {
                    var user = new User()
                    {
                        Name = jMe.name
                    };
                    string fbUserId = jMe.id;
                    var fbUser = new AuthSystemUser("Facebook", fbUserId, accessToken);
                    var realUser = Authenticator.Value.Auth(user, fbUser);

                    authResult.Id = realUser.Id;
                    authResult.Name = realUser.Name;
                    authResult.Token = realUser.Token;

                    var cookieOptions = new CookieOptions()
                    {
                        Domain = "localhost:7788"
                    };

                    Response.Cookies.Append("auth.token", realUser.Token, cookieOptions);
                }
            }

            return PartialView("AuthResultPartial", authResult);
        }

        private dynamic GetJson(string uri)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(uri);
                var res = response.Result;
                if (res.IsSuccessStatusCode)
                {
                    var bytes = res.Content.ReadAsByteArrayAsync().Result;
                    var str = Encoding.UTF8.GetString(bytes);
                    return JObject.Parse(str);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
