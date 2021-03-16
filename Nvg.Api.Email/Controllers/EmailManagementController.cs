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

        public EmailManagementController(IEmailManagementInteractor emailManagementInteractor, IEmailInteractor emailInteractor,ILogger<EmailManagementController> logger)
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
            try
            {
                var poolResponse = _emailManagementInteractor.UpdateEmailPool(poolInput);
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
                _logger.LogError("Internal server error: Error occurred while updating email pool: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{poolID}")]
        public ActionResult DeleteEmailPool(string poolID)
        {
            _logger.LogInformation("DeleteEmailPool action method.");
            _logger.LogDebug("Pool ID: " + poolID);
            try
            {
                var poolResponse = _emailManagementInteractor.DeleteEmailPool(poolID);
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
                _logger.LogError("Internal server error: Error occurred while deleting email pool: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        #endregion

        #region Email Provider
        [HttpGet("{poolName}")]
        public ActionResult GetEmailProviders(string poolName)
        {
            _logger.LogInformation("GetEmailProviders action method.");
            _logger.LogDebug("Pool Name: " + poolName);
            try
            {
                var providerResponse = _emailManagementInteractor.GetEmailProviders(poolName);
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

        [HttpGet("{poolName}")]
        public ActionResult GetEmailProviderNames(string poolName)
        {
            _logger.LogInformation("GetEmailProviderNames action method.");
            _logger.LogDebug("Pool Name: " + poolName);
            try
            {
                var providerResponse = _emailManagementInteractor.GetEmailProviderNames(poolName);
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
        public ActionResult UpdateEmailProvider(EmailProviderSettingsDto providerInput)
        {
            _logger.LogInformation("UpdateEmailProviders action method.");
            _logger.LogDebug("Pool Name: " + providerInput.EmailPoolName);
            try
            {
                var providerResponse = _emailManagementInteractor.UpdateEmailProvider(providerInput);
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
                _logger.LogError("Internal server error: Error occurred while updating email providers: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{providerID}")]
        public ActionResult DeleteEmailProvider(string providerID)
        {
            _logger.LogInformation("DeleteEmailProvider action method.");
            _logger.LogDebug("Provider ID: " + providerID);
            try
            {
                var providerResponse = _emailManagementInteractor.DeleteEmailProvider(providerID);
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
                _logger.LogError("Internal server error: Error occurred while deleting email pool: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{poolName}/{providerName?}")]
        public ActionResult GetEmailChannelsByPool(string poolName, string providerName = null)
        {
            _logger.LogInformation("GetEmailChannelsByPool action method.");
            _logger.LogDebug("Pool Name: " + poolName);
            try
            {
                var channelResponse = _emailManagementInteractor.GetEmailChannelsByPool(poolName,providerName);
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
        public ActionResult UpdateEmailChannel(EmailChannelDto channelInput)
        {
            _logger.LogInformation("UpdateEmailChannel action method.");
            _logger.LogDebug("Pool Name: " + channelInput.EmailPoolName);
            try
            {
                var channelResponse = _emailManagementInteractor.UpdateEmailChannel(channelInput);
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
                _logger.LogError("Internal server error: Error occurred while updating email channel: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{channelID}")]
        public ActionResult DeleteEmailChannel(string channelID)
        {
            _logger.LogInformation("DeleteEmailChannel action method.");
            _logger.LogDebug("Channel ID: " + channelID);
            try
            {
                var channelResponse = _emailManagementInteractor.DeleteEmailChannel(channelID);
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
                _logger.LogError("Internal server error: Error occurred while deleting email channel: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        #endregion

        #region Email Template
        [HttpGet("{poolName}")]
        public ActionResult GetEmailTemplatesByPool(string poolName)
        {
            _logger.LogInformation("GetEmailTemplatesByPool action method.");
            _logger.LogDebug("Pool Name: " + poolName);
            try
            {
                var templateResponse = _emailManagementInteractor.GetEmailTemplatesByPool(poolName);
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
        public ActionResult DeleteEmailTemplate(string templateID)
        {
            _logger.LogInformation("DeleteEmailTemplate action method.");
            _logger.LogDebug("Tempalte ID: " + templateID);
            try
            {
                var templateResponse = _emailManagementInteractor.DeleteEmailTemplate(templateID);
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
                _logger.LogError("Internal server error: Error occurred while deleting email template: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        #endregion

    }
}
