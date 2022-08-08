using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure
{
    public class HeaderVerificationMiddleware
    {
        RequestDelegate _next;
        public HeaderVerificationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var tvalue = context.Request.Headers.TryGetValue("passwordKey", out var traceValue);

            if (traceValue == "passwordKey123456789")
            {
                await _next.Invoke(context);
            }
            else
            {

                context.Response.StatusCode = 403; //Unauthorized
                return;
            }
        }
    }
}
