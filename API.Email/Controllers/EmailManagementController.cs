using API.Email.Models;
using AutoMapper;
using EmailService.DTOS;
using EmailService.Email;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;

namespace API.Email.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin")]
    public class EmailManagementController : ControllerBase
    {
        private readonly IEmailManagementInteractor _emailManagementInteractor;
        private readonly ILogger<EmailManagementController> _logger;
        private readonly IMapper _mapper;
        private IConfiguration _config;

        public EmailManagementController(IEmailManagementInteractor emailManagementInteractor,
            IConfiguration config, ILogger<EmailManagementController> logger, IMapper mapper)
        {
            _emailManagementInteractor = emailManagementInteractor;
            _logger = logger;
            _mapper = mapper;
            _config = config;
        }

        #region Email Pool
        /// <summary>
        /// API to get all the Email Pool data.
        /// </summary>
        /// <returns><see cref="EmailResponseDto{T}"></see></returns>
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

        /// <summary>
        /// API to get all the Email Pool Names.
        /// </summary>
        /// <returns><see cref="EmailResponseDto{T}"></see></returns>
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

        /// <summary>
        /// API to add email pool to the database table.
        /// </summary>
        /// <param name="poolInput"><see cref="PoolInput"/> model</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        [HttpPost]
        public ActionResult AddEmailPool(PoolInput poolInput)
        {
            _logger.LogInformation("AddEmailPool action method.");
            _logger.LogDebug("EmailPoolName: " + poolInput.Name);
            EmailResponseDto<EmailPoolDto> poolResponse = new EmailResponseDto<EmailPoolDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(poolInput.Name))
                {
                    var mappedInput = _mapper.Map<EmailPoolDto>(poolInput);
                    poolResponse = _emailManagementInteractor.AddEmailPool(mappedInput);
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
        /// API to update the Email Pool data.
        /// </summary>
        /// <param name="poolInput"><see cref="PoolMgmtInput"/></param>
        /// <returns><see cref="EmailResponseDto{T}"></see></returns>
        [HttpPost]
        public ActionResult UpdateEmailPool(PoolMgmtInput poolInput)
        {
            _logger.LogInformation("UpdateEmailPool action method.");
            EmailResponseDto<EmailPoolDto> poolResponse = new EmailResponseDto<EmailPoolDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(poolInput.Name))
                {
                    var mappedInput = _mapper.Map<EmailPoolDto>(poolInput);
                    poolResponse = _emailManagementInteractor.UpdateEmailPool(mappedInput);
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

        /// <summary>
        /// API to delete the Email Pool data.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="EmailResponseDto{T}"></see></returns>
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

        /// <summary>
        /// API to get the Email Providers.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="EmailResponseDto{T}"></see></returns>
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

        /// <summary>
        /// API to get the Email Providers Names.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
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

        /// <summary>
        /// API to add the Email Providers.
        /// </summary>
        /// <param name="providerInput"><see cref="ProviderInput"/> model</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        [HttpPost]
        public ActionResult AddEmailProvider(ProviderInput providerInput)
        {
            _logger.LogInformation("AddEmailProvider action method.");
            _logger.LogDebug("Pool Name: " + providerInput.EmailPoolName);
            EmailResponseDto<EmailProviderSettingsDto> providerResponse = new EmailResponseDto<EmailProviderSettingsDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(providerInput.Configuration) && !string.IsNullOrWhiteSpace(providerInput.Type) && !string.IsNullOrWhiteSpace(providerInput.Name))
                {
                    var mappedInput = _mapper.Map<EmailProviderSettingsDto>(providerInput);
                    providerResponse = _emailManagementInteractor.AddEmailProvider(mappedInput);
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

        /// <summary>
        /// API to update the Email Providers.
        /// </summary>
        /// <param name="providerInput"><see cref="ProviderMgmtInput"/> model</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        [HttpPost]
        public ActionResult UpdateEmailProvider(ProviderMgmtInput providerInput)
        {
            _logger.LogInformation("UpdateEmailProviders action method.");
            _logger.LogDebug("Pool Name: " + providerInput.EmailPoolName);
            EmailResponseDto<EmailProviderSettingsDto> providerResponse = new EmailResponseDto<EmailProviderSettingsDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(providerInput.Configuration))
                {
                    var mappedInput = _mapper.Map<EmailProviderSettingsDto>(providerInput);
                    providerResponse = _emailManagementInteractor.UpdateEmailProvider(mappedInput);
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

        /// <summary>
        /// API to delete the Email Providers.
        /// </summary>
        /// <param name="providerID">Email Provider ID</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
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

        /// <summary>
        /// API to get the Email Channels by Pool.
        /// </summary>
        /// <param name="poolID">Email Pool ID</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
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

        /// <summary>
        /// API to get all the Email Channel Key data.
        /// </summary>
        /// <returns><see cref="EmailResponseDto{T}"></see></returns>
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

        /// <summary>
        /// API to add the Email Channel.
        /// </summary>
        /// <param name="channelInput"><see cref="ChannelInput"/></param>
        /// <returns><see cref="EmailResponseDto{T}"></see></returns>
        [HttpPost]
        public ActionResult AddEmailChannel(ChannelInput channelInput)
        {
            _logger.LogInformation("AddEmailChannel action method.");
            _logger.LogDebug("Pool Name: " + channelInput.EmailPoolName);
            EmailResponseDto<EmailChannelDto> channelResponse = new EmailResponseDto<EmailChannelDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(channelInput.Key))
                {
                    var mappedInput = _mapper.Map<EmailChannelDto>(channelInput);
                    channelResponse = _emailManagementInteractor.AddEmailChannel(mappedInput);
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
        /// API to update the Email Channel.
        /// </summary>
        /// <param name="channelInput"><see cref="ChannelMgmtInput"/></param>
        /// <returns><see cref="EmailResponseDto{T}"></see></returns>
        [HttpPost]
        public ActionResult UpdateEmailChannel(ChannelMgmtInput channelInput)
        {
            _logger.LogInformation("UpdateEmailChannel action method.");
            _logger.LogDebug("Pool Name: " + channelInput.EmailPoolName);
            EmailResponseDto<EmailChannelDto> channelResponse = new EmailResponseDto<EmailChannelDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(channelInput.Key))
                {
                    var mappedInput = _mapper.Map<EmailChannelDto>(channelInput);
                    channelResponse = _emailManagementInteractor.UpdateEmailChannel(mappedInput);
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

        /// <summary>
        /// API to delete the Email Channel data.
        /// </summary>
        /// <param name="channelID">Email Channel ID</param>
        /// <returns><see cref="EmailResponseDto{T}"></see></returns>
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
        /// <summary>
        /// API to get all the Email Templates by Pool ID.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="EmailResponseDto{T}"></see></returns>
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

        /// <summary>
        /// API to get all the Email Templates by Channel ID.
        /// </summary>
        /// <param name="channelID">Email Channel ID</param>
        /// <returns><see cref="EmailResponseDto{T}"></see></returns>
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

        /// <summary>
        /// API to get the Email Tempalte by Template ID.
        /// </summary>
        /// <param name="templateID">Template ID</param>
        /// <returns><see cref="EmailResponseDto{T}"></see></returns>
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

        /// <summary>
        /// API to add the Email Template data.
        /// </summary>
        /// <param name="templateInput"><see cref="TemplateInput"/></param>
        /// <returns><see cref="EmailResponseDto{T}"></see></returns>
        [HttpPost]
        public ActionResult AddEmailTemplate(TemplateInput templateInput)
        {
            _logger.LogInformation("AddEmailTemplate action method.");
            _logger.LogDebug("Pool ID: " + templateInput.EmailPoolID);
            EmailResponseDto<EmailTemplateDto> templateResponse = new EmailResponseDto<EmailTemplateDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(templateInput.Name) && !string.IsNullOrWhiteSpace(templateInput.MessageTemplate))
                {
                    var mappedInput = _mapper.Map<EmailTemplateDto>(templateInput);
                    templateResponse = _emailManagementInteractor.AddEmailTemplate(mappedInput);
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

        /// <summary>
        /// API to update the Email Template data.
        /// </summary>
        /// <param name="templateInput"><see cref="TemplateMgmtInput"/></param>
        /// <returns><see cref="EmailResponseDto{T}"></see></returns>
        [HttpPost]
        public ActionResult UpdateEmailTemplate(TemplateMgmtInput templateInput)
        {
            _logger.LogInformation("UpdateEmailTemplate action method.");
            _logger.LogDebug("Pool ID: " + templateInput.EmailPoolID);
            EmailResponseDto<EmailTemplateDto> templateResponse = new EmailResponseDto<EmailTemplateDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(templateInput.Name) && !string.IsNullOrWhiteSpace(templateInput.MessageTemplate))
                {
                    var mappedInput = _mapper.Map<EmailTemplateDto>(templateInput);
                    templateResponse = _emailManagementInteractor.UpdateEmailTemplate(mappedInput);
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

        /// <summary>
        /// API to delete the Email Template data.
        /// </summary>
        /// <param name="templateID">Template ID</param>
        /// <returns><see cref="EmailResponseDto{T}"></see></returns>
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

        #region email histories
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

        #region Email Quota
        /// <summary>
        /// API to gets the Email Quota.
        /// </summary>
        /// <param name="channelID">Channel ID</param>
        /// <returns><see cref="EmailResponseDto{T}"></see></returns>
        [HttpGet("{channelID}")]
        public ActionResult GetEmailQuotaList(string channelID)
        {
            _logger.LogInformation("GetEmailQuotaList action method.");
            _logger.LogDebug("Channel ID: " + channelID);
            EmailResponseDto<List<EmailQuotaDto>> channelResponse = new EmailResponseDto<List<EmailQuotaDto>>();
            try
            {
                if (!string.IsNullOrWhiteSpace(channelID))
                {
                    channelResponse = _emailManagementInteractor.GetEmailQuotaList(channelID);
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
                _logger.LogError("Internal server error: Error occurred while getting email quota: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to add the Email Quota.
        /// </summary>
        /// <param name="channelInput"><see cref="ChannelInput"/></param>
        /// <returns><see cref="EmailResponseDto{T}"></see></returns>
        [HttpPost]
        public ActionResult AddEmailQuota(ChannelInput channelInput)
        {
            _logger.LogInformation("AddEmailChannel action method.");
            _logger.LogDebug("Pool Name: " + channelInput.EmailPoolName);
            EmailResponseDto<EmailQuotaDto> channelResponse = new EmailResponseDto<EmailQuotaDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(channelInput.Key))
                {
                    var mappedInput = _mapper.Map<EmailChannelDto>(channelInput);
                    channelResponse = _emailManagementInteractor.AddEmailQuota(mappedInput);
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
                _logger.LogError("Internal server error: Error occurred while adding email quota: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to update the Email Quota.
        /// </summary>
        /// <param name="channelInput"><see cref="ChannelInput"/></param>
        /// <returns><see cref="EmailResponseDto{T}"></see></returns>
        [HttpPost]
        public ActionResult UpdateEmailQuota(ChannelInput channelInput)
        {
            _logger.LogInformation("UpdateEmailQuota action method.");
            _logger.LogDebug("Pool Name: " + channelInput.EmailPoolName);
            EmailResponseDto<EmailQuotaDto> channelResponse = new EmailResponseDto<EmailQuotaDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(channelInput.Key))
                {
                    var mappedInput = _mapper.Map<EmailChannelDto>(channelInput);
                    channelResponse = _emailManagementInteractor.UpdateEmailQuota(mappedInput);
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
                _logger.LogError("Internal server error: Error occurred while updating email quota: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to Delete the Email Quota.
        /// </summary>
        /// <param name="channelID">Channel ID</param>
        /// <returns><see cref="EmailResponseDto{T}"></see></returns>
        [HttpDelete("{channelID}")]
        public ActionResult DeleteEmailQuota(string channelID)
        {
            _logger.LogInformation("DeleteEmailQuota action method.");
            _logger.LogDebug("Channel ID: " + channelID);
            EmailResponseDto<string> channelResponse = new EmailResponseDto<string>();
            try
            {
                if (!string.IsNullOrWhiteSpace(channelID))
                {
                    channelResponse = _emailManagementInteractor.DeleteEmailQuota(channelID);
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
                _logger.LogError("Internal server error: Error occurred while deleting email quota: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        #endregion

        /// <summary>
        /// API to send emails.
        /// </summary>
        /// <param name="emailInputs"><see cref="EmailInput"/> model</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        [HttpPost]
        public ActionResult SendMail(EmailInput emailInputs)
        {
            _logger.LogInformation("SendMail action method.");
            //_logger.LogDebug($"Recipients: {emailInputs.Recipients}, ChannelKey: {emailInputs.ChannelKey}, Body: {emailInputs.Body}, Sender: {emailInputs.Sender}, TemplateName: {emailInputs.TemplateName}");
            try
            {
                var mappedInput = _mapper.Map<EmailDto>(emailInputs);
                var emailResponse = _emailManagementInteractor.SendMail(mappedInput);
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
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: Error occurred while trying to send email: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Gets the API document URL.
        /// </summary>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        [HttpGet]
        public IActionResult GetApiDocumentUrl()
        {
            CustomResponse<string> response = new CustomResponse<string>();
            string url = _config.GetSection("apiDocumentDownloadUrl").Value;
            response.Status = true;
            response.Message = "Retrieved URL.";
            response.Result = url;
            return Ok(response);
        }
    }
}
