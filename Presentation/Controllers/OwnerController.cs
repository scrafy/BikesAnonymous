using System;
using System.Threading.Tasks;
using BikesAnonymous.Models.Common;
using BikesAnonymous.Models.OwnerControllerModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OwnerCMD.Interfaces;



namespace BikesAnonymous.Controllers
{
    
    public class OwnerController : BaseController
    {
        private readonly IOwnerCommandProvider _ownerCommand;

        public OwnerController(IOwnerCommandProvider ownerCommand)
        {
            _ownerCommand = ownerCommand;
        }

        [HttpPost]
        [Route("loadcsvfile")]
        public async Task<ActionResult> LoadCyclistCSVFile(IFormFile file)
        {
            if(file != null && file.Length > 0)
            {
                var buffer = new byte[file.Length];
                file.OpenReadStream().Read(buffer, 0, (int)file.Length);
                try
                {
                    await _ownerCommand.GetLoadCSVFileCommand().LoadCSVDataAsync(buffer);
                    return StatusCode(500, "OK");

                }
                catch(Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
               
            }
            return StatusCode(500, "badfile");

        }

        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous()]
        public async Task<IActionResult> Authenticate(AuthenticateDTO credentials)
        {
            return Ok(WrapResponse<string>(await _ownerCommand.GetOwnerAuthenticateCommand().AuthenticateAsync(credentials.Username, credentials.Password), null));
        }

                
        [AllowAnonymous()]
        [HttpPost()]
        public async Task<IActionResult> Post(OwnerDTO owner)
        {
            await _ownerCommand.OwnerCreateAccountCommand().CreateAccountAsync(owner.ToDomain());
            return Ok(WrapResponse<string>(null, null));
        }
       
    }
}
