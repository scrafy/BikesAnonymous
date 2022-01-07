using System;
using System.IO;
using System.Threading.Tasks;
using BikesAnonymous.Models.Common;
using Core.Enums;
using Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OwnerCMD.Interfaces;



namespace BikesAnonymous.Controllers
{
    
    public class CyclistController : BaseController
    {
        private readonly ICyclistCommandProvider _cyclistCommand;

        public CyclistController(ICyclistCommandProvider cyclistCommand)
        {
            _cyclistCommand = cyclistCommand;
        }

  
        [HttpGet]
        [Route("printlicense")]
        [Authorize(Roles = "CYCLIST")]
        public async Task<IActionResult> PrintLicense()
        {
            var cyclistId = GetClaimFromJWTToken("Id") ?? throw new GenericException("Claim Id not found in JWT token", ErrorCode.BAD_REQUEST);
            var data = await _cyclistCommand.GetPrintLicenseCommand().PrintLicenseAsync(new Guid(cyclistId));
            return File(new MemoryStream(data), "application/pdf", "license.pdf");
        }

        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous()]
        public async Task<IActionResult> Authenticate(AuthenticateDTO credentials)
        {
            return Ok(WrapResponse<string>(await _cyclistCommand.GetCyclistAuthenticateCommand().AuthenticateAsync(credentials.Username, credentials.Password), null));
        }
       
    }
}
