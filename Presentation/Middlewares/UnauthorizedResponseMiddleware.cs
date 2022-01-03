using BikesAnonymous.Models;
using BikesAnonymous.Models.Common;
using Core.Enums;
using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace BikesAnonymous.MiddleWares
{
    public class UnauthorizedResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public UnauthorizedResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            await _next(context);

            if ((HttpStatusCode)context.Response.StatusCode == HttpStatusCode.Unauthorized || 
                (HttpStatusCode)context.Response.StatusCode == HttpStatusCode.NotFound || 
                (HttpStatusCode)context.Response.StatusCode == HttpStatusCode.MethodNotAllowed)
            {
                var result = new ServerResponseDTO<string>
                {
                    ServerError = new GenericException(((HttpStatusCode)context.Response.StatusCode).ToString(), (ErrorCode)(context.Response.StatusCode)).GetServerInfoError(),                    
                    StatusCode = (short) context.Response.StatusCode

                };

                context.Response.ContentType = "application/json";
                await HttpResponseWritingExtensions.WriteAsync(context.Response, JsonConvert.SerializeObject(result));

            }
           
            
        }
    }
}
