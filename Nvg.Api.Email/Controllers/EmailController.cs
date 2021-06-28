using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeTypes;
using Newtonsoft.Json;
using Nvg.Api.Email.Models;
using Nvg.EmailService.DTOS;
using Nvg.EmailService.Email;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Nvg.Api.Email.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailInteractor _emailInteractor;
        private readonly ILogger<EmailController> _logger;
        private readonly IMapper _mapper;
        private readonly string defaultEmailChannel = "MasterEmailChannel";

        public EmailController(IEmailInteractor emailInteractor, ILogger<EmailController> logger, IMapper mapper)
        {
            _emailInteractor = emailInteractor;
            _logger = logger;
            _mapper = mapper;
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
                    poolResponse = _emailInteractor.AddEmailPool(mappedInput);
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
        /// <param name="providerInput"><see cref="ProviderInput"/> model</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        [HttpPost]
        public ActionResult AddEmailProvider(ProviderInput providerInput)
        {
            _logger.LogInformation("AddEmailProvider action method.");
            _logger.LogDebug($"EmailPoolName: {providerInput.EmailPoolName}, EmailProviderName: {providerInput.Name}, Configuration: {providerInput.Configuration}");
            EmailResponseDto<EmailProviderSettingsDto> providerResponse = new EmailResponseDto<EmailProviderSettingsDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(providerInput.Configuration) && !string.IsNullOrWhiteSpace(providerInput.Type) && !string.IsNullOrWhiteSpace(providerInput.Name))
                {
                    var mappedInput = _mapper.Map<EmailProviderSettingsDto>(providerInput);
                    providerResponse = _emailInteractor.AddEmailProvider(mappedInput);
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
        /// API to update email provider to the database table.
        /// </summary>
        /// <param name="providerInput"><see cref="ProviderInput"/> model</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        [HttpPost]
        public ActionResult UpdateEmailProvider(ProviderInput providerInput)
        {
            _logger.LogInformation("UpdateEmailProvider action method.");
            _logger.LogDebug($"EmailPoolName: {providerInput.EmailPoolName}, EmailProviderName: {providerInput.Name}, Configuration: {providerInput.Configuration}");
            try
            {
                var mappedInput = _mapper.Map<EmailProviderSettingsDto>(providerInput);
                var providerResponse = _emailInteractor.UpdateEmailProvider(mappedInput);
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
                _logger.LogError("Internal server error: Error occurred while updating email provider: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to add Email Channel to the database table.
        /// </summary>
        /// <param name="channelInput"><see cref="ChannelInput"/> model</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        [HttpPost]
        public ActionResult AddEmailChannel(ChannelInput channelInput)
        {
            _logger.LogInformation("AddEmailChannel action method.");
            _logger.LogDebug($"EmailPoolName: {channelInput.EmailPoolName}, EmailProviderName: {channelInput.EmailProviderName}");
            EmailResponseDto<EmailChannelDto> channelResponse = new EmailResponseDto<EmailChannelDto>();
            try
            {
                if (!string.IsNullOrWhiteSpace(channelInput.Key))
                {
                    var mappedInput = _mapper.Map<EmailChannelDto>(channelInput);
                    channelResponse = _emailInteractor.AddEmailChannel(mappedInput);
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
        /// API to update Email Channel to the database table.
        /// </summary>
        /// <param name="channelInput"><see cref="ChannelInput"/> model</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        [HttpPost]
        public ActionResult UpdateEmailChannel(ChannelInput channelInput)
        {
            _logger.LogInformation("UpdateEmailChannel action method.");
            _logger.LogDebug($"EmailPoolName: {channelInput.EmailPoolName}, EmailProviderName: {channelInput.EmailProviderName}");
            try
            {
                var mappedInput = _mapper.Map<EmailChannelDto>(channelInput);
                var channelResponse = _emailInteractor.UpdateEmailChannel(mappedInput);
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

        /// <summary>
        /// API to add email template to the database table.
        /// </summary>
        /// <param name="templateInput"><see cref="TemplateInput"/> model</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        [HttpPost]
        public ActionResult AddEmailTemplate(TemplateInput templateInput)
        {
            _logger.LogInformation("AddEmailTemplate action method.");
            _logger.LogDebug($"EmailPoolName: {templateInput.EmailPoolName}, TemplateName: {templateInput.Name}, Variant: {templateInput.Variant}, MessageTemplate: {templateInput.MessageTemplate}");
            EmailResponseDto<EmailTemplateDto> templateResponse = new EmailResponseDto<EmailTemplateDto>();
            try
            {
               
                if (!string.IsNullOrWhiteSpace(templateInput.Name) && !string.IsNullOrWhiteSpace(templateInput.MessageTemplate))
                {
                    var mappedInput = _mapper.Map<EmailTemplateDto>(templateInput);
                    templateResponse = _emailInteractor.AddEmailTemplate(mappedInput);
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
        /// API to Update email template to the database table.
        /// </summary>
        /// <param name="templateInput"><see cref="TemplateInput"/> model</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        [HttpPost]
        public ActionResult UpdateEmailTemplate(TemplateInput templateInput)
        {
            _logger.LogInformation("UpdateEmailTemplate action method.");
            _logger.LogDebug($"EmailPoolName: {templateInput.EmailPoolName}, TemplateName: {templateInput.Name}, Variant: {templateInput.Variant}, MessageTemplate: {templateInput.MessageTemplate}");
            try
            {
                var mappedInput = _mapper.Map<EmailTemplateDto>(templateInput);
                var templateResponse = _emailInteractor.UpdateEmailTemplate(mappedInput);
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
                _logger.LogError("Internal server error: Error occurred while updating email template: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API to get the Email Channel by channel key.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        [HttpGet("{channelKey}")]
        public ActionResult GetEmailChannelByKey(string channelKey)
        {
            _logger.LogInformation("GetEmailChannelByKey action method.");
            _logger.LogDebug($"ChannelKey: {channelKey}");
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
        /// API to get default Channel.
        /// </summary>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        [HttpGet]
        public ActionResult GetDefaultChannnel()
        {
            _logger.LogInformation("GetDefaultChannnel action method .");
            try
            {
                var channelResponse = _emailInteractor.GetEmailChannelByKey(defaultEmailChannel);
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
                _logger.LogError("Internal server error: Error occurred while getting default email channel: " + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }

        }
        /// <summary>
        /// API to get the email providers by pool name and providers name.
        /// </summary>
        /// <param name="poolName">Pool Name</param>
        /// <param name="providerName">Providers Name</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        [HttpGet("{poolName}/{providerName}")]
        public ActionResult GetEmailProvidersByPool(string poolName, string providerName)
        {
            _logger.LogInformation("GetEmailProvidersByPool action method.");
            _logger.LogDebug($"PoolName: {poolName}, ProviderName: {providerName}");
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
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        [HttpGet("{channelKey}/{tag?}")]
        public ActionResult GetEmailHistories(string channelKey, string tag = null)
        {
            _logger.LogInformation("GetEmailHistories action method.");
            _logger.LogDebug($"ChannelKey: {channelKey}, Tag: {tag}");
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
                var emailResponse = _emailInteractor.SendMail(mappedInput);
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

        /// <summary>
        /// API to send emails with Attachments. Attachments to be added to FormFileCollection. Email input to be sent along with Form Input with key as "EmailInput" . 
        /// </summary>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        [HttpPost]
        public ActionResult SendMailWithAttachments()
        {
            _logger.LogInformation("SendMailWithAttachments action method.");
            //_logger.LogDebug($"Recipients: {emailInputs.Recipients}, ChannelKey: {emailInputs.ChannelKey}, Body: {emailInputs.Body}, Sender: {emailInputs.Sender}, TemplateName: {emailInputs.TemplateName}");
            try
            {
                var files = ( Request.HasFormContentType && Request.Form.Files.Any() ) ? Request.Form.Files : new FormFileCollection();
                var json = Request.Form["EmailInput"];
                if (string.IsNullOrEmpty(json))
                {
                    var errorResponse = new CustomResponse<string>();
                    errorResponse.Message = "Email Input Must Be Provied";
                    errorResponse.Status = false;
                    _logger.LogError("Status: " + errorResponse.Status + ", " + errorResponse.Message);
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, errorResponse);
                }
                var emailInputs = JsonConvert.DeserializeObject<EmailInput>(json);
                var mappedInput = _mapper.Map<EmailDto>(emailInputs);
                mappedInput.Files = new List<EmailAttachment>();
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                var fileBytes = ms.ToArray();
                                var fileContent = Convert.ToBase64String(fileBytes);
                                mappedInput.Files.Add(new EmailAttachment { ContentType = MimeTypeMap.GetMimeType(file.FileName), FileContent = fileContent, FileName = file.FileName });
                            }
                        }
                    }
                }
                var emailResponse = _emailInteractor.SendMailWithAttachments(mappedInput);
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
    }
}