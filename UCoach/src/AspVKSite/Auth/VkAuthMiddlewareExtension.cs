
using Microsoft.AspNetCore.Builder;

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
