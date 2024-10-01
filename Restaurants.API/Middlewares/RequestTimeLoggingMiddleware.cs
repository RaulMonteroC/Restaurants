using System.Diagnostics;

namespace Restaurants.API.Middlewares;

public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        const short endpointThreshold = 4000;
        var timer = new Stopwatch();
        timer.Start();

        await next.Invoke(context);
        
        timer.Stop();

        if (timer.ElapsedMilliseconds > endpointThreshold)
        {
            logger.LogWarning("Request at [{verb} {path}] took {timer} ms", 
                              context.Request.Method,
                              context.Request.Path,
                              timer.ElapsedMilliseconds);
        }
        
    }
}