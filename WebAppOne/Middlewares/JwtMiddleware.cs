namespace NewsPortal.API.Middlewares
{
    public class JwtMiddleware
    {
        private readonly IDistributedCache _cache;
        private readonly RequestDelegate _next;


        public JwtMiddleware(RequestDelegate next, IDistributedCache cache)
        {
            _next = next;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var authorizationPayload = context.Request.Headers.Authorization;
            if (authorizationPayload.Count == 0)
            {
                await _next.Invoke(context);
                return;
            }

            var accessToken = authorizationPayload.ToString();
            if (string.IsNullOrEmpty(accessToken))
            {
                await _next.Invoke(context);
                return;
            }

            var deniedToken = await _cache.GetRecordAsync<string>(accessToken);
            if (string.IsNullOrEmpty(deniedToken))
            {
                await _next.Invoke(context);
                return;
            }

            context.Response.StatusCode = 403;
        }
    }
}
