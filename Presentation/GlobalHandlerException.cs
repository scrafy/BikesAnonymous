using BikesAnonymous.Models.Common;
using Core.Enums;
using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;


namespace BikesAnonymous
{
    public class GlobalHandlerException
    {

        public GlobalHandlerException()
        {

        }
               

        public async void HandleException(Exception ex, HttpContext context)
        {
            ServerResponseDTO<string> result = null;           
            if(ex.GetType().BaseType == typeof(BaseException))
            {
                var baseException = (BaseException)ex;
                result = new ServerResponseDTO<string>
                {
                    ServerError = baseException.GetServerInfoError(),
                    StatusCode = (short)baseException.ErrorCode

                };
            }
            else
            {
                result = new ServerResponseDTO<string>
                {
                    ServerError = new
                    {

                        ErrorMessage = ex.Message,
                        ErrorStackTrace = ex.StackTrace,
                        ErrorCode = (short)ErrorCode.INTERNAL_SERVER_ERROR

                    },
                    StatusCode = (short)ErrorCode.INTERNAL_SERVER_ERROR

                };
            }
            context.Response.StatusCode = result.StatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }
}
