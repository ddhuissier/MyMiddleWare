using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMiddleWare
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public MyMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger("MyLogger");
            _logger.LogInformation("MyMiddleware constructor !");
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation("MyMiddleware Invoke !");
            await httpContext.Response.Body.WriteAsync(Encoding.ASCII.GetBytes("Hello Invoke !"));
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {
            return builder
                .UseMiddleware<MyMiddleware>();
                // transfer to AppMiddlewareExtensions
                //.UseWhen(context => 
                //    context.Request.Query.ContainsKey("role"), a => {
                //        a.Run(async c => {
                //            await c.Response.WriteAsync($"The role is {c.Request.Query["role"]}");
                //    });
                //})
                //.Map("/map", a => {
                //        a.Run(async c => {
                //            await c.Response.WriteAsync($"/map endpoint call !!");
                //        });
                //    }); ;
        }
    }
}
