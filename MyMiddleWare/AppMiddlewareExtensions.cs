using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMiddleWare
{
    public static class AppMiddlewareExtensions
    {
        public static void UseExtensions(this IApplicationBuilder app)
        {
            app.UseWhen(context =>
                    context.Request.Query.ContainsKey("role"), a => {
                        a.Run(async c => {
                            await c.Response.WriteAsync($"The role is {c.Request.Query["role"]}");
                        });
                    })
                .Map("/map", a => {
                    a.Run(async c => {
                        await c.Response.WriteAsync($"/map endpoint call !!");
                    });
                });
        }
    }
}
