using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using TraversalServices.Interfaces;
using TraversalServices.Models;

namespace BikesAnonymous.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITraversalServicesProvider _traversalServiceProvider;

        public TestController(ITraversalServicesProvider traversalServiceProvider)
        {
            _traversalServiceProvider = traversalServiceProvider;
        }

        [HttpGet]
        public async Task<FileResult> Get()
        {
            var obj = new Cyclist(Guid.NewGuid(), "username", "password", Core.Enums.ROLE.CYCLIST, DateTime.Now, DateTime.Now.AddDays(1), Guid.NewGuid(), "Firstname", "LastName", 25, "scrafy26@gmail.com");
            var pdf = _traversalServiceProvider.GetPDFGeneratorService().ObjectToPDFAsync(obj);
            var attachments = new Dictionary<string, byte[]>();
            attachments.Add($"License.pdf", pdf.Result);
            _traversalServiceProvider.GetEmailService().SendEmailAsync(new EmailSetting()
            {
                To = new string[] { obj.Email },
                Body = "Your account has been created correctly on Bikes Anonymous association",
                Subject = "Bikes Anonymous Sing Up",
                Attachments = attachments
            });

            
            return File(pdf.Result, "application/pdf", "License.pdf");
        }
    }
}
