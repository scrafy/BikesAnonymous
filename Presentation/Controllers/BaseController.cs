using Microsoft.AspNetCore.Authorization;
using BikesAnonymous.Models.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Core.Exceptions;
using Core.Enums;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;

namespace BikesAnonymous.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    
    public abstract class BaseController : ControllerBase
    {
        protected virtual ServerResponseDTO<T> WrapResponse<T>(T input, PaginationDTO paginationData) where T : class
        {
            return new ServerResponseDTO<T>()
            {
                StatusCode = 200,
                ServerData = new ServerDataDTO<T>()
                {
                    Data = input,
                    PaginationData = paginationData
                },
                ServerError = null
            };
        }

        protected virtual string GetClaimFromJWTToken(string claim)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
                return identity.Claims.FirstOrDefault(c => c.Type == claim)?.Value;

            return null;
        }

    }
}
