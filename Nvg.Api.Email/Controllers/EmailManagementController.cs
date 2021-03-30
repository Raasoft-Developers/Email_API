using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nvg.EmailService.DTOS;
using Nvg.EmailService.Email;

namespace Nvg.Api.Email.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmailManagementController : ControllerBase
    {
        private readonly IEmailManagementInteractor _emailManagementInteractor;
        private readonly IEmailInteractor _emailInteractor;
        private readonly ILogger<EmailManagementController> _logger;

        public EmailManagementController(IEmailManagementInteractor emailManagementInteractor, IEmailInteractor emailInteractor, ILogger<EmailManagementController> logger)
        {
            _emailManagementInteractor = emailManagementInteractor;
            _emailInteractor = emailInteractor;
            _logger = logger;
        }

        #region Email Pool
        [HttpGet]
        public ActionResult GetEmailPools()
        {
            _logger.LogInformation("GetEmailPools action method.");
            try
            {
                var poolResponse = _emailManagementInteractor.GetEmailPools();
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
                _logger.LogError("Internal server error: Error occurred while getting email pools: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        public ActionResult GetEmailPoolNames()
        {
            _logger.LogInformation("GetEmailPoolNames action method.");
            try
            {
                var poolResponse = _emailManagementInteractor.GetEmailPoolNames();
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
                _logger.LogError("Internal server error: Error occurred while getting email pool names: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public ActionResult UpdateEmailPool(EmailPoolDto poolInput)
        {
            _logger.LogInformation("UpdateEmailPool action method.");
            EmailResponseDto<EmailPoolDto> poolResponse = new EmailResponseDto<EmailPoolDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(poolInput.Name))
                {
                    poolResponse = _emailManagementInteractor.UpdateEmailPool(poolInput);
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
                _logger.LogError("Internal server error: Error occurred while updating email pool: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{poolID}")]
        public ActionResult DeleteEmailPool(string poolID)
        {
            _logger.LogInformation("DeleteEmailPool action method.");
            _logger.LogDebug("Pool ID: " + poolID);
            EmailResponseDto<string> poolResponse = new EmailResponseDto<string>();
            try
            {
                if (!string.IsNullOrWhiteSpace(poolID))
                {
                    poolResponse = _emailManagementInteractor.DeleteEmailPool(poolID);
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
                    poolResponse.Message = "Pool ID cannot be empty or whitespace.";
                    _logger.LogError("Status: " + poolResponse.Status + ", " + poolResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, poolResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while deleting email pool: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        #endregion

        #region Email Provider
        [HttpGet("{poolID}")]
        public ActionResult GetEmailProviders(string poolID)
        {
            _logger.LogInformation("GetEmailProviders action method.");
            _logger.LogDebug("Pool ID: " + poolID);
            try
            {
                var providerResponse = _emailManagementInteractor.GetEmailProviders(poolID);
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
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while getting email providers: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{poolID}")]
        public ActionResult GetEmailProviderNames(string poolID)
        {
            _logger.LogInformation("GetEmailProviderNames action method.");
            _logger.LogDebug("Pool Name: " + poolID);
            try
            {
                var providerResponse = _emailManagementInteractor.GetEmailProviderNames(poolID);
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
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while getting email provider names: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public ActionResult AddEmailProvider(EmailProviderSettingsDto providerInput)
        {
            _logger.LogInformation("AddEmailProvider action method.");
            _logger.LogDebug("Pool Name: " + providerInput.EmailPoolName);
            EmailResponseDto<EmailProviderSettingsDto> providerResponse = new EmailResponseDto<EmailProviderSettingsDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(providerInput.Configuration) && !string.IsNullOrWhiteSpace(providerInput.Type) && !string.IsNullOrWhiteSpace(providerInput.Name))
                {
                    providerResponse = _emailManagementInteractor.AddUpdateEmailProvider(providerInput);
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
                _logger.LogError("Internal server error: Error occurred while updating email providers: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public ActionResult UpdateEmailProvider(EmailProviderSettingsDto providerInput)
        {
            _logger.LogInformation("UpdateEmailProviders action method.");
            _logger.LogDebug("Pool Name: " + providerInput.EmailPoolName);
            EmailResponseDto<EmailProviderSettingsDto> providerResponse = new EmailResponseDto<EmailProviderSettingsDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(providerInput.Configuration))
                {
                    providerResponse = _emailManagementInteractor.AddUpdateEmailProvider(providerInput);
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
                    providerResponse.Message = "Configuration cannot be empty or whitespace.";
                    _logger.LogError("Status: " + providerResponse.Status + ", " + providerResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, providerResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while updating email providers: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{providerID}")]
        public ActionResult DeleteEmailProvider(string providerID)
        {
            _logger.LogInformation("DeleteEmailProvider action method.");
            _logger.LogDebug("Provider ID: " + providerID);
            EmailResponseDto<string> providerResponse = new EmailResponseDto<string>();
            try
            {
                if (!string.IsNullOrWhiteSpace(providerID))
                {
                    providerResponse = _emailManagementInteractor.DeleteEmailProvider(providerID);
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
                    providerResponse.Message = "Provider ID cannot be empty or whitespace.";
                    _logger.LogError("Status: " + providerResponse.Status + ", " + providerResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, providerResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while deleting email provider: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{poolID}")]
        public ActionResult GetEmailChannelsByPool(string poolID)
        {
            _logger.LogInformation("GetEmailChannelsByPool action method.");
            _logger.LogDebug("Pool Name: " + poolID);
            try
            {
                var channelResponse = _emailManagementInteractor.GetEmailChannelsByPool(poolID);
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
                _logger.LogError("Internal server error: Error occurred while geting email channels: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        public ActionResult GetEmailChannelKeys()
        {
            _logger.LogInformation("GetEmailChannelKeys action method.");
            try
            {
                var channelResponse = _emailManagementInteractor.GetEmailChannelKeys();
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
                _logger.LogError("Internal server error: Error occurred while getting email channel keys: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public ActionResult AddEmailChannel(EmailChannelDto channelInput)
        {
            _logger.LogInformation("AddEmailChannel action method.");
            _logger.LogDebug("Pool Name: " + channelInput.EmailPoolName);
            EmailResponseDto<EmailChannelDto> channelResponse = new EmailResponseDto<EmailChannelDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(channelInput.Key))
                {
                    channelResponse = _emailManagementInteractor.AddUpdateEmailChannel(channelInput);
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

        [HttpPost]
        public ActionResult UpdateEmailChannel(EmailChannelDto channelInput)
        {
            _logger.LogInformation("UpdateEmailChannel action method.");
            _logger.LogDebug("Pool Name: " + channelInput.EmailPoolName);
            EmailResponseDto<EmailChannelDto> channelResponse = new EmailResponseDto<EmailChannelDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(channelInput.Key))
                {
                    channelResponse = _emailManagementInteractor.AddUpdateEmailChannel(channelInput);
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
                _logger.LogError("Internal server error: Error occurred while updating email channel: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{channelID}")]
        public ActionResult DeleteEmailChannel(string channelID)
        {
            _logger.LogInformation("DeleteEmailChannel action method.");
            _logger.LogDebug("Channel ID: " + channelID);
            EmailResponseDto<string> channelResponse = new EmailResponseDto<string>();
            try
            {
                if (!string.IsNullOrWhiteSpace(channelID))
                {
                    channelResponse = _emailManagementInteractor.DeleteEmailChannel(channelID);
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
                    channelResponse.Message = "Channel ID cannot be empty or whitespace.";
                    _logger.LogError("Status: " + channelResponse.Status + ", " + channelResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, channelResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while deleting email channel: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        #endregion

        #region Email Template
        [HttpGet("{poolID}")]
        public ActionResult GetEmailTemplatesByPool(string poolID)
        {
            _logger.LogInformation("GetEmailTemplatesByPool action method.");
            _logger.LogDebug("Pool Name: " + poolID);
            try
            {
                var templateResponse = _emailManagementInteractor.GetEmailTemplatesByPool(poolID);
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
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while getting email templates by pool: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{channelID}")]
        public ActionResult GetEmailTemplatesByChannelID(string channelID)
        {
            _logger.LogInformation("GetEmailTemplatesByChannelID action method.");
            _logger.LogDebug("Channel ID: " + channelID);
            try
            {
                var templateResponse = _emailManagementInteractor.GetEmailTemplatesByChannelID(channelID);
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
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while getting email templates by pool: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{templateID}")]
        public ActionResult GetEmailTemplateByID(string templateID)
        {
            _logger.LogInformation("GetEmailTemplateByID action method.");
            _logger.LogDebug("Template ID: " + templateID);
            try
            {
                var templateResponse = _emailManagementInteractor.GetEmailTemplate(templateID);
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
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while getting email templates by id: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public ActionResult AddEmailTemplate(EmailTemplateDto templateInput)
        {
            _logger.LogInformation("AddEmailTemplate action method.");
            _logger.LogDebug("Pool ID: " + templateInput.EmailPoolID);
            EmailResponseDto<EmailTemplateDto> templateResponse = new EmailResponseDto<EmailTemplateDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(templateInput.Name) && !string.IsNullOrWhiteSpace(templateInput.MessageTemplate))
                {
                    templateResponse = _emailManagementInteractor.AddUpdateEmailTemplate(templateInput);
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
                else
                {
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

        [HttpPost]
        public ActionResult UpdateEmailTemplate(EmailTemplateDto templateInput)
        {
            _logger.LogInformation("UpdateEmailTemplate action method.");
            _logger.LogDebug("Pool ID: " + templateInput.EmailPoolID);
            EmailResponseDto<EmailTemplateDto> templateResponse = new EmailResponseDto<EmailTemplateDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(templateInput.Name) && !string.IsNullOrWhiteSpace(templateInput.MessageTemplate))
                {
                    templateResponse = _emailManagementInteractor.AddUpdateEmailTemplate(templateInput);
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
                else
                {
                    templateResponse.Status = false;
                    templateResponse.Message = "Name and Message Template cannot be empty or whitespace.";
                    _logger.LogError("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, templateResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while updating email template: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{templateID}")]
        public ActionResult DeleteEmailTemplate(string templateID)
        {
            _logger.LogInformation("DeleteEmailTemplate action method.");
            _logger.LogDebug("Tempalte ID: " + templateID);
            EmailResponseDto<string> templateResponse = new EmailResponseDto<string>();
            try
            {
                if (!string.IsNullOrWhiteSpace(templateID))
                {
                    templateResponse = _emailManagementInteractor.DeleteEmailTemplate(templateID);
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
                else
                {
                    templateResponse.Status = false;
                    templateResponse.Message = "Template ID cannot be empty or whitespace.";
                    _logger.LogError("Status: " + templateResponse.Status + ", " + templateResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, templateResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while deleting email template: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        #endregion

        #region email histores
        /// <summary>
        /// API to get the email history by channel key and tag.
        /// </summary>
        /// <param name="channelID">Channel ID</param>
        /// <param name="tag">Tag</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        [HttpGet("{channelID}/{tag?}")]
        public ActionResult GetEmailHistories(string channelID, string tag = null)
        {
            _logger.LogInformation("GetEmailHistories action method.");
            _logger.LogInformation($"ChannelKey: {channelID}, Tag: {tag}");
            try
            {
                var historiesResponse = _emailManagementInteractor.GetEmailHistories(channelID, tag);
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
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while trying to get email histories: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        #endregion


        /// <summary>
        /// API to send emails.
        /// </summary>
        /// <param name="emailInputs"><see cref="EmailDto"/> model</param>
        /// <returns><see cref="EmailResponseDto{string}"/></returns>
        //[HttpPost]
        //public ActionResult SendMail(EmailDto emailInputs)
        //{
        //    _logger.LogInformation("SendMail action method.");
        //    //_logger.LogInformation($"Recipients: {emailInputs.Recipients}, ChannelKey: {emailInputs.ChannelKey}, Body: {emailInputs.Body}, Sender: {emailInputs.Sender}, TemplateName: {emailInputs.TemplateName}");
        //    try
        //    {
        //        var emailResponse = _emailInteractor.SendMail(emailInputs);
        //        if (emailResponse.Status)
        //        {
        //            _logger.LogDebug("Status: " + emailResponse.Status + ", " + emailResponse.Message);
        //            return Ok(emailResponse);
        //        }
        //        else
        //        {
        //            _logger.LogError("Status: " + emailResponse.Status + ", " + emailResponse.Message);
        //            return StatusCode((int)HttpStatusCode.PreconditionFailed, emailResponse);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Internal server error: Error occurred while trying to send email: " + ex.Message);
        //        return StatusCode((int)HttpStatusCode.InternalServerError, ex);
        //    }
        //}
    }
}
