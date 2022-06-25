using Microsoft.Extensions.Primitives;

namespace ComicBookInventory.Api.Middleware
{
    public class SecurityHeaders
    {
        private readonly RequestDelegate _next;

        public SecurityHeaders(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("", new StringValues("enable"));
            return _next(context);
        }
    }
}
