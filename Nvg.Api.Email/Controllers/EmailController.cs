using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nvg.EmailService;
using Nvg.EmailService.Dtos;

namespace Nvg.Api.Email.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailInteractor _emailInteractor;

        public EmailController(IEmailInteractor emailInteractor)
        {
            _emailInteractor = emailInteractor;
        }

        [HttpPost]
        public ActionResult SendMail(EmailDto emailInputs)
        {
            CustomeResponse<string> response = new CustomeResponse<string>();
            //bool sent = _emailProvider.SendEmail(email.To, email.MailBody, email.Subject, email.HtmlContent, email.Sender).Result;
            _emailInteractor.SendMail(emailInputs);
            response.Status = true;
            response.Message = $"Email is sent successfully to {emailInputs.To} ";
            response.Result = "SENT";
            return Ok(response);
        }
    }
}