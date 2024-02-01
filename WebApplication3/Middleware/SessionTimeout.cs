using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WebApplication3.Model;

public class SessionTimeoutMiddleware
{
    private readonly RequestDelegate _next;
    private readonly TimeSpan _sessionTimeout;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SessionTimeoutMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
    {
        _next = next;
        _sessionTimeout = TimeSpan.FromSeconds(30); // Set this to your session timeout
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Debug.WriteLine("SessionTimeoutMiddleware running");

        var sessionStartTimeString = context.Request.Cookies["SessionStartTime"];
        Debug.WriteLine($"Session start time string: {sessionStartTimeString}");
        if (!string.IsNullOrEmpty(sessionStartTimeString) && DateTime.TryParse(sessionStartTimeString, out var sessionStartTime))
        {
            var elapsedTime = DateTime.UtcNow - sessionStartTime;
            Debug.WriteLine($"Session elapsed time: {elapsedTime.TotalSeconds} seconds");
            if (elapsedTime > _sessionTimeout)
            {
                // Session has expired
                context.Response.Cookies.Delete("SessionStartTime"); // Delete the cookie
                context.Response.Cookies.Delete("Email");
                var signInManager = context.RequestServices.GetRequiredService<SignInManager<CustomUser>>();
                Debug.WriteLine("Session expired, logging out user");
                await signInManager.SignOutAsync();

                context.Response.Redirect("/Login"); // Redirect to login page
                return;
            }
        }

        // Call the next delegate/middleware in the pipeline
        await _next(context);
    }
}

public static class SessionTimeoutMiddlewareExtensions
{
    public static IApplicationBuilder UseSessionTimeout(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SessionTimeoutMiddleware>();
    }
}
