using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AniX_BusinessLogic
{
    public class CustomAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Session.Keys.Contains("UserId"))
            {
                var identity = new GenericIdentity(context.Session.GetString("UserId"), "CustomAuthScheme");
                context.User = new GenericPrincipal(identity, null);
            }
            await _next(context);
        }
    }
}
