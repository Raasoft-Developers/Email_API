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

        /// <summary>
        /// API to add email pool to the database table.
        /// </summary>
        /// <param name="poolInput"><see cref="EmailPoolDto"/> model</param>
        /// <returns><see cref="EmailResponseDto{EmailPoolDto}"/></returns>
        [HttpPost]
        public ActionResult AddEmailPool(EmailPoolDto poolInput)
        {
            var poolResponse = _emailInteractor.AddEmailPool(poolInput);
            if (poolResponse.Status)
                return Ok(poolResponse);
            else
                return StatusCode((int)HttpStatusCode.PreconditionFailed, poolResponse);
        }

        /// <summary>
        /// API to add email provider to the database table.
        /// </summary>
        /// <param name="providerInput"><see cref="EmailProviderSettingsDto"/> model</param>
        /// <returns><see cref="EmailResponseDto{EmailProviderSettingsDto}"/></returns>
        [HttpPost]
        public ActionResult AddEmailProvider(EmailProviderSettingsDto providerInput)
        {
            var providerResponse = _emailInteractor.AddEmailProvider(providerInput);
            if (providerResponse.Status)
                return Ok(providerResponse);
            else
                return StatusCode((int)HttpStatusCode.PreconditionFailed, providerResponse);
        }

        /// <summary>
        /// API to add Email Channel to the database table.
        /// </summary>
        /// <param name="channelInput"><see cref="EmailChannelDto"/> model</param>
        /// <returns><see cref="EmailResponseDto{EmailChannelDto}"/></returns>
        [HttpPost]
        public ActionResult AddEmailChannel(EmailChannelDto channelInput)
        {
            var channelResponse = _emailInteractor.AddEmailChannel(channelInput);
            if (channelResponse.Status)
                return Ok(channelResponse);
            else
                return StatusCode((int)HttpStatusCode.PreconditionFailed, channelResponse);
        }

        /// <summary>
        /// API to add email template to the database table.
        /// </summary>
        /// <param name="templateInput"><see cref="EmailTemplateDto"/> model</param>
        /// <returns><see cref="EmailResponseDto{EmailTemplateDto}"/></returns>
        [HttpPost]
        public ActionResult AddEmailTemplate(EmailTemplateDto templateInput)
        {
            var templateResponse = _emailInteractor.AddEmailTemplate(templateInput);
            if (templateResponse.Status)
                return Ok(templateResponse);
            else
                return StatusCode((int)HttpStatusCode.PreconditionFailed, templateResponse);
        }

        /// <summary>
        /// API to get the Email Channel by channel key.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="EmailResponseDto{EmailChannelDto}"/></returns>
        [HttpGet("{channelKey}")]
        public ActionResult GetEmailChannelByKey(string channelKey)
        {
            var channelResponse = _emailInteractor.GetEmailChannelByKey(channelKey);
            if (channelResponse.Status)
                return Ok(channelResponse);
            else
                return StatusCode((int)HttpStatusCode.PreconditionFailed, channelResponse);
        }

        /// <summary>
        /// API to get the email providers by pool name and providers name.
        /// </summary>
        /// <param name="poolName">Pool Name</param>
        /// <param name="providerName">Providers Name</param>
        /// <returns><see cref="EmailResponseDto{List{EmailProviderSettingsDto}}"/></returns>
        [HttpGet("{poolName}/{providerName}")]
        public ActionResult GetEmailProvidersByPool(string poolName, string providerName)
        {
            var poolResponse = _emailInteractor.GetEmailProvidersByPool(poolName, providerName);
            if (poolResponse.Status)
                return Ok(poolResponse);
            else
                return StatusCode((int)HttpStatusCode.PreconditionFailed, poolResponse);
        }

        /// <summary>
        /// API to get the email history by channel key and tag.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="tag">Tag</param>
        /// <returns><see cref="EmailResponseDto{List{EmailHistoryDto}}"/></returns>
        [HttpGet("{channelKey}/{tag?}")]
        public ActionResult GetEmailHistories(string channelKey, string tag = null)
        {
            var historiesResponse = _emailInteractor.GetEmailHistoriesByTag(channelKey, tag);
            if (historiesResponse.Status)
                return Ok(historiesResponse);
            else
                return StatusCode((int)HttpStatusCode.PreconditionFailed, historiesResponse);
        }

        /// <summary>
        /// API to send emails.
        /// </summary>
        /// <param name="emailInputs"><see cref="EmailDto"/> model</param>
        /// <returns><see cref="EmailResponseDto{string}"/></returns>
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