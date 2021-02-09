using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nvg.EmailService;
using Nvg.EmailService.Email;
using Nvg.EmailService.DTOS;
using System.Net;

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
        public ActionResult AddEmailPool(EmailPoolDto poolInput)
        {
            var poolResponse = _emailInteractor.AddEmailPool(poolInput);
            if (poolResponse.Status)
                return Ok(poolResponse);
            else
                return StatusCode((int)HttpStatusCode.PreconditionFailed, poolResponse);
        }

        [HttpPost]
        public ActionResult AddEmailProvider(EmailProviderSettingsDto providerInput)
        {
            var providerResponse = _emailInteractor.AddEmailProvider(providerInput);
            if (providerResponse.Status)
                return Ok(providerResponse);
            else
                return StatusCode((int)HttpStatusCode.PreconditionFailed, providerResponse);
        }

        [HttpPost]
        public ActionResult AddEmailChannel(EmailChannelDto channelInput)
        {
            var channelResponse = _emailInteractor.AddEmailChannel(channelInput);
            if (channelResponse.Status)
                return Ok(channelResponse);
            else
                return StatusCode((int)HttpStatusCode.PreconditionFailed, channelResponse);
        }

        [HttpPost]
        public ActionResult AddEmailTemplate(EmailTemplateDto templateInput)
        {
            var templateResponse = _emailInteractor.AddEmailTemplate(templateInput);
            if (templateResponse.Status)
                return Ok(templateResponse);
            else
                return StatusCode((int)HttpStatusCode.PreconditionFailed, templateResponse);
        }

        [HttpGet]
        public ActionResult GetEmailChannelByKey(string channelKey)
        {
            var channelResponse = _emailInteractor.GetEmailChannelByKey(channelKey);
            if (channelResponse.Status)
                return Ok(channelResponse);
            else
                return StatusCode((int)HttpStatusCode.PreconditionFailed, channelResponse);
        }

        [HttpGet]
        public ActionResult GetEmailProvidersByPool(string poolName, string providerName)
        {
            var poolResponse = _emailInteractor.GetEmailProvidersByPool(poolName, providerName);
            if (poolResponse.Status)
                return Ok(poolResponse);
            else
                return StatusCode((int)HttpStatusCode.PreconditionFailed, poolResponse);
        }

        [HttpGet]
        public ActionResult GetEmailHistories(string channelKey, string tag = null)
        {
            var historiesResponse = _emailInteractor.GetEmailHistoriesByTag(channelKey, tag);
            if (historiesResponse.Status)
                return Ok(historiesResponse);
            else
                return StatusCode((int)HttpStatusCode.PreconditionFailed, historiesResponse);
        }

        [HttpPost]
        public ActionResult SendMail(EmailDto emailInputs)
        {            
            var emailResponse = _emailInteractor.SendMail(emailInputs);
            if (emailResponse.Status)
                return Ok(emailResponse);
            else
                return StatusCode((int)HttpStatusCode.PreconditionFailed, emailResponse);
        }
    }
}