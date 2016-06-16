using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;

namespace AspSite.Controllers
{
    [Route("api/[controller]")]
    public class FacebookController : Controller
    {
        [HttpGet]
        public void Login()
        {
            var uri = string.Format(
                "https://www.facebook.com/dialog/oauth?client_id={0}&redirect_uri={1}&scope={2}",
                "1034374253265538",
                "http://localhost:7788/api/facebook/code/",
                "public_profile");

            Response.Redirect(uri);
        }

        [HttpGet("code")]
        public void FacebookCode(string code)
        {
            var tokenUri = string.Format(
                  "https://graph.facebook.com/v2.5/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}",
                   "1034374253265538",
                   "http://localhost:7788/api/facebook/code/",  // !!!
                   "0d5ba10073168902817f1be6a477b12a",
                   code);

            var jToken = this.ExecuteJsonQuery(tokenUri);
            if (jToken != null)
            {
                string accessToken = jToken.access_token;
                var meUri = string.Format(
                    "https://graph.facebook.com/me?access_token={0}",
                    accessToken);

                var jMe = this.ExecuteJsonQuery(meUri);
                //if (jMe != null)
                //{
                //    var user = new User()
                //    {
                //        Name = jMe.name
                //    };
                //    string fbUserId = jMe.id;
                //    var fbUser = new AuthSystemUser("Facebook", fbUserId, accessToken);
                //    var realUser = Authenticator.Value.Auth(user, fbUser);

                //    var cookieOptions = new CookieOptions()
                //    {
                //        Domain = "localhost:7788"
                //    };

                //    Response.Cookies.Append("auth.token", realUser.Token, cookieOptions);
                //}
            }
        }

        private dynamic ExecuteJsonQuery(string uri)
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
