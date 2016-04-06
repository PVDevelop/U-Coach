using Microsoft.AspNet.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspVKSite.Auth
{
    public static class VkAuthMiddlewareExtension
    {
        public static IApplicationBuilder UseVkAuth(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<VKAuthMiddleware>();
        }
    }
}
