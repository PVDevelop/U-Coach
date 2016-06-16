using AspSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspSite.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        [HttpPost]
        public void Logon([FromBody]LogonViewModel logonViewModel)
        {

        }
    }
}
