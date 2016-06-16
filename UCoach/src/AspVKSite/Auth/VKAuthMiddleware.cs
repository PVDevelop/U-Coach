using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AspVKSite.Auth
{
    public class VKAuthMiddleware
    {
        private readonly RequestDelegate _next;
        public VKAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers["Authorization"] == "Basic aGFicmE6aGFicg==")
            {
                context.Response.StatusCode = 200;
                await _next.Invoke(context);
            }
            else
            {
                context.Response.StatusCode = 401;
                context.Response.Headers.Add("WWW-Authenticate", "Basic realm=\"localhost\"");
            }
        }
    }
}
