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
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Nvg.Api.Email.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmailController : ControllerBase
    {
        private readonly IEmailInteractor _emailInteractor;
        private readonly ILogger<EmailController> _logger;
        private IConfiguration _config;

        public EmailController(IEmailInteractor emailInteractor, ILogger<EmailController> logger, IConfiguration config)
        {
            _emailInteractor = emailInteractor;
            _logger = logger;
            _config = config;
        }

        /// <summary>
        /// API to add email pool to the database table.
        /// </summary>
        /// <param name="poolInput"><see cref="EmailPoolDto"/> model</param>
        /// <returns><see cref="EmailResponseDto{EmailPoolDto}"/></returns>
        [HttpPost]
        public ActionResult AddEmailPool(EmailPoolDto poolInput)
        {
            _logger.LogInformation("AddEmailPool action method.");
            _logger.LogInformation("EmailPoolName: " + poolInput.Name);
            EmailResponseDto<EmailPoolDto> poolResponse = new EmailResponseDto<EmailPoolDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(poolInput.Name))
                {
                    poolResponse = _emailInteractor.AddEmailPool(poolInput);
                    if (poolResponse.Status)
                    {
                        _logger.LogDebug("Status: " + poolResponse.Status + ", " + poolResponse.Message);
                        return Ok(poolResponse);
                    }
                    else
                    {
                        _logger.LogError("Status: " + poolResponse.Status + ", " + poolResponse.Message);
                        return StatusCode((int)HttpStatusCode.PreconditionFailed, poolResponse);
                    }
                }
                else
                {
                    poolResponse.Status = false;
                    poolResponse.Message = "Pool Name cannot be empty or whitespace.";
                    _logger.LogError("Status: " + poolResponse.Status + ", " + poolResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, poolResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while adding email pool: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to add email provider to the database table.
        /// </summary>
        /// <param name="providerInput"><see cref="EmailProviderSettingsDto"/> model</param>
        /// <returns><see cref="EmailResponseDto{EmailProviderSettingsDto}"/></returns>
        [HttpPost]
        public ActionResult AddEmailProvider(EmailProviderSettingsDto providerInput)
        {
            _logger.LogInformation("AddEmailProvider action method.");
            _logger.LogInformation($"EmailPoolName: {providerInput.EmailPoolName}, EmailProviderName: {providerInput.Name}, Configuration: {providerInput.Configuration}");
            EmailResponseDto<EmailProviderSettingsDto> providerResponse = new EmailResponseDto<EmailProviderSettingsDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(providerInput.Configuration) && !string.IsNullOrWhiteSpace(providerInput.Type) && !string.IsNullOrWhiteSpace(providerInput.Name))
                {
                    providerResponse = _emailInteractor.AddEmailProvider(providerInput);
                    if (providerResponse.Status)
                    {
                        _logger.LogDebug("Status: " + providerResponse.Status + ", " + providerResponse.Message);
                        return Ok(providerResponse);
                    }
                    else
                    {
                        _logger.LogError("Status: " + providerResponse.Status + ", " + providerResponse.Message);
                        return StatusCode((int)HttpStatusCode.PreconditionFailed, providerResponse);
                    }
                }
                else
                {
                    providerResponse.Status = false;
                    providerResponse.Message = "Provider Name, Type and Configuration cannot be empty or whitespace.";
                    _logger.LogError("Status: " + providerResponse.Status + ", " + providerResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, providerResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while adding email provider: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to add Email Channel to the database table.
        /// </summary>
        /// <param name="channelInput"><see cref="EmailChannelDto"/> model</param>
        /// <returns><see cref="EmailResponseDto{EmailChannelDto}"/></returns>
        [HttpPost]
        public ActionResult AddEmailChannel(EmailChannelDto channelInput)
        {
            _logger.LogInformation("AddEmailChannel action method.");
            _logger.LogInformation($"EmailPoolName: {channelInput.EmailPoolName}, EmailProviderName: {channelInput.EmailProviderName}");
            EmailResponseDto<EmailChannelDto> channelResponse = new EmailResponseDto<EmailChannelDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(channelInput.Key))
                {
                    channelResponse = _emailInteractor.AddEmailChannel(channelInput);
                    if (channelResponse.Status)
                    {
                        _logger.LogDebug("Status: " + channelResponse.Status + ", " + channelResponse.Message);
                        return Ok(channelResponse);
                    }
                    else
                    {
                        _logger.LogError("Status: " + channelResponse.Status + ", " + channelResponse.Message);
                        return StatusCode((int)HttpStatusCode.PreconditionFailed, channelResponse);
                    }
                }
                else
                {
                    channelResponse.Status = false;
                    channelResponse.Message = "Channel Key cannot be empty or whitespace.";
                    _logger.LogError("Status: " + channelResponse.Status + ", " + channelResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, channelResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while adding email channel: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to add email template to the database table.
        /// </summary>
        /// <param name="templateInput"><see cref="EmailTemplateDto"/> model</param>
        /// <returns><see cref="EmailResponseDto{EmailTemplateDto}"/></returns>
        [HttpPost]
        public ActionResult AddEmailTemplate(EmailTemplateDto templateInput)
        {
            _logger.LogInformation("AddEmailTemplate action method.");
            _logger.LogInformation($"EmailPoolName: {templateInput.EmailPoolName}, TemplateName: {templateInput.Name}, Variant: {templateInput.Variant}, MessageTemplate: {templateInput.MessageTemplate}");
            EmailResponseDto<EmailTemplateDto> templateResponse = new EmailResponseDto<EmailTemplateDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(templateInput.Name) && !string.IsNullOrWhiteSpace(templateInput.MessageTemplate))
                {
                    templateResponse = _emailInteractor.AddEmailTemplate(templateInput);
                    if (templateResponse.Status)
                    {
                        _logger.LogDebug("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                        return Ok(templateResponse);
                    }
                    else
                    {
                        _logger.LogError("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                        return StatusCode((int)HttpStatusCode.PreconditionFailed, templateResponse);
                    }
                }
                else{
                    templateResponse.Status = false;
                    templateResponse.Message = "Name and Message Template cannot be empty or whitespace.";
                    _logger.LogError("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, templateResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while adding email template: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to get the Email Channel by channel key.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="EmailResponseDto{EmailChannelDto}"/></returns>
        [HttpGet("{channelKey}")]
        public ActionResult GetEmailChannelByKey(string channelKey)
        {
            _logger.LogInformation("GetEmailChannelByKey action method.");
            _logger.LogInformation($"ChannelKey: {channelKey}");
            try
            {
                var channelResponse = _emailInteractor.GetEmailChannelByKey(channelKey);
                if (channelResponse.Status)
                {
                    _logger.LogDebug("Status: " + channelResponse.Status + ", " + channelResponse.Message);
                    return Ok(channelResponse);
                }
                else
                {
                    _logger.LogError("Status: " + channelResponse.Status + ", " + channelResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, channelResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while getting email channel by key: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
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
            _logger.LogInformation("GetEmailProvidersByPool action method.");
            _logger.LogInformation($"PoolName: {poolName}, ProviderName: {providerName}");
            try
            {
                var poolResponse = _emailInteractor.GetEmailProvidersByPool(poolName, providerName);
                if (poolResponse.Status)
                {
                    _logger.LogDebug("Status: " + poolResponse.Status + ", " + poolResponse.Message);
                    return Ok(poolResponse);
                }
                else
                {
                    _logger.LogError("Status: " + poolResponse.Status + ", " + poolResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, poolResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while trying to get email provider by pool: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
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
            _logger.LogInformation("GetEmailHistories action method.");
            _logger.LogInformation($"ChannelKey: {channelKey}, Tag: {tag}");
            try
            {
                var historiesResponse = _emailInteractor.GetEmailHistoriesByTag(channelKey, tag);
                if (historiesResponse.Status)
                {
                    _logger.LogDebug("Status: " + historiesResponse.Status + ", " + historiesResponse.Message);
                    return Ok(historiesResponse);
                }
                else
                {
                    _logger.LogError("Status: " + historiesResponse.Status + ", " + historiesResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, historiesResponse);
                }
            }catch(Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while trying to get email histories: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to send emails.
        /// </summary>
        /// <param name="emailInputs"><see cref="EmailDto"/> model</param>
        /// <returns><see cref="EmailResponseDto{string}"/></returns>
        [HttpPost]
        public ActionResult SendMail(EmailDto emailInputs)
        {
            _logger.LogInformation("SendMail action method.");
            //_logger.LogInformation($"Recipients: {emailInputs.Recipients}, ChannelKey: {emailInputs.ChannelKey}, Body: {emailInputs.Body}, Sender: {emailInputs.Sender}, TemplateName: {emailInputs.TemplateName}");
            try
            {
                var emailResponse = _emailInteractor.SendMail(emailInputs);
                if (emailResponse.Status)
                {
                    _logger.LogDebug("Status: " + emailResponse.Status + ", " + emailResponse.Message);
                    return Ok(emailResponse);
                }
                else
                {
                    _logger.LogError("Status: " + emailResponse.Status + ", " + emailResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, emailResponse);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while trying to send email: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        public IActionResult GetApiDocumentUrl()
        {
            CustomResponse<string> response = new CustomResponse<string>();
            string url = _config.GetSection("apiDocumentDownloadUrl").Value;
            response.Status = true;
            response.Message = "retrieved URl";
            response.Result = url;
            return Ok(response);
        }
    }
}