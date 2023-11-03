using System.Security.Claims;
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
                var userId = context.Session.GetString("UserId");
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userId)
                };
                var identity = new ClaimsIdentity(claims, "CustomAuthScheme");
                context.User = new ClaimsPrincipal(identity);
            }
            await _next(context);
        }

    }
}
