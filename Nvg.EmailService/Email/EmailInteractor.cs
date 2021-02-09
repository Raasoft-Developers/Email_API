using Nvg.EmailService.DTOS;
using Nvg.EmailService.EmailChannel;
using Nvg.EmailService.EmailHistory;
using Nvg.EmailService.EmailPool;
using Nvg.EmailService.EmailProvider;
using Nvg.EmailService.EmailTemplate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.Email
{
    public class EmailInteractor : IEmailInteractor
    {
        private readonly IEmailEventInteractor _emailEventInteractor;
        private readonly IEmailPoolInteractor _emailPoolInteractor;
        private readonly IEmailProviderInteractor _emailProviderInteractor;
        private readonly IEmailChannelInteractor _emailChannelInteractor;
        private readonly IEmailTemplateInteractor _emailTemplateInteractor;
        private readonly IEmailHistoryInteractor _emailHistoryInteractor;

        public EmailInteractor(IEmailEventInteractor emailEventInteractor,
            IEmailPoolInteractor emailPoolInteractor, IEmailProviderInteractor emailProviderInteractor,
            IEmailChannelInteractor emailChannelInteractor, IEmailTemplateInteractor emailTemplateInteractor,
            IEmailHistoryInteractor emailHistoryInteractor)
        {
            _emailEventInteractor = emailEventInteractor;
            _emailPoolInteractor = emailPoolInteractor;
            _emailProviderInteractor = emailProviderInteractor;
            _emailChannelInteractor = emailChannelInteractor;
            _emailTemplateInteractor = emailTemplateInteractor;
            _emailHistoryInteractor = emailHistoryInteractor;
        }

        public EmailResponseDto<EmailPoolDto> AddEmailPool(EmailPoolDto poolInput)
        {
            var poolResponse = _emailPoolInteractor.AddEmailPool(poolInput);
            return poolResponse;
        }

        public EmailResponseDto<EmailProviderSettingsDto> AddEmailProvider(EmailProviderSettingsDto providerInput)
        {
            var providerResponse = _emailProviderInteractor.AddEmailProvider(providerInput);
            return providerResponse;
        }

        public EmailResponseDto<EmailChannelDto> AddEmailChannel(EmailChannelDto channelInput)
        {
            var channelResponse = _emailChannelInteractor.AddEmailChannel(channelInput);
            return channelResponse;
        }

        public EmailResponseDto<EmailTemplateDto> AddEmailTemplate(EmailTemplateDto templateInput)
        {
            var templateResponse = _emailTemplateInteractor.AddEmailTemplate(templateInput);
            return templateResponse;
        }

        public EmailResponseDto<EmailChannelDto> GetEmailChannelByKey(string channelKey)
        {
            var channelResponse = _emailChannelInteractor.GetEmailChannelByKey(channelKey);
            return channelResponse;
        }

        public EmailResponseDto<List<EmailProviderSettingsDto>> GetEmailProvidersByPool(string poolName, string providerName)
        {
            var poolResponse = _emailProviderInteractor.GetEmailProvidersByPool(poolName, providerName);
            return poolResponse;
        }

        public EmailResponseDto<List<EmailHistoryDto>> GetEmailHistoriesByTag(string channelKey, string tag)
        {
            var poolResponse = _emailHistoryInteractor.GetEmailHistoriesByTag(channelKey, tag);
            return poolResponse;
        }

        public EmailResponseDto<string> SendMail(EmailDto emailInputs)
        {
            var response = new EmailResponseDto<string>();
            try
            {
                if (string.IsNullOrEmpty(emailInputs.ChannelKey))
                {
                    response.Status = false;
                    response.Message = "Channel key cannot be blank.";
                    return response;
                }
                else
                {
                    var channelExist = _emailChannelInteractor.CheckIfChannelExist(emailInputs.ChannelKey).Result;
                    if (!channelExist)
                    {
                        response.Status = channelExist;
                        response.Message = $"Invalid Channel key {emailInputs.ChannelKey}.";
                        return response;
                    }
                }
                if (string.IsNullOrEmpty(emailInputs.TemplateName))
                {
                    response.Status = false;
                    response.Message = "Template name cannot be blank.";
                    return response;
                }
                else
                {
                    var templateExist = _emailTemplateInteractor.CheckIfTemplateExist(emailInputs.ChannelKey, emailInputs.TemplateName).Result;
                    if (!templateExist)
                    {
                        response.Status = templateExist;
                        response.Message = $"No template found for template name {emailInputs.TemplateName} and channel key {emailInputs.ChannelKey}.";
                        return response;
                    }
                }
                _emailEventInteractor.SendMail(emailInputs);
                response.Status = true;
                response.Message = $"Email is sent successfully to {emailInputs.Recipients}.";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
