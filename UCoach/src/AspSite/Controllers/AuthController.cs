using AspSite.Models;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspSite.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        [HttpGet]
        public void GetAuthSystems()
        {
            //var token = Request.Cookies["token"].FirstOrDefault();
            //if (token == null)
            //{
            //    // пользователь не авторизован, начинаем запрос
            //    // перенаправляем пока на Facebook, потом надо на страницу аутентификации
            //}

            Response.Redirect("http://localhost:7788/api/facebook/");
        }
    }
}
