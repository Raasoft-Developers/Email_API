using Nvg.EmailService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.Email
{
    public interface IEmailInteractor
    {
        /// <summary>
        /// Method to send an email.
        /// </summary>
        /// <param name="emailInputs"><see cref="EmailDto"/> model</param>
        /// <returns><see cref="EmailResponseDto{string}"/></returns>
        EmailResponseDto<string> SendMail(EmailDto emailInputs);

        /// <summary>
        /// Adds the email pool to database.
        /// </summary>
        /// <param name="poolInput"><see cref="EmailPoolDto"/> model</param>
        /// <returns><see cref="EmailResponseDto{EmailPoolDto}"/></returns>
        EmailResponseDto<EmailPoolDto> AddEmailPool(EmailPoolDto poolInput);

        /// <summary>
        /// Adds the email provider to the database.
        /// </summary>
        /// <param name="providerInput"><see cref="EmailProviderSettingsDto"/></param>
        /// <returns><see cref="EmailResponseDto{EmailProviderSettingsDto}"/></returns>
        EmailResponseDto<EmailProviderSettingsDto> AddEmailProvider(EmailProviderSettingsDto providerInput);

        /// <summary>
        /// Adds the email channel to the database.
        /// </summary>
        /// <param name="channelInput"><see cref="EmailChannelDto"/> model</param>
        /// <returns><see cref="EmailResponseDto{EmailChannelDto}"/></returns>
        EmailResponseDto<EmailChannelDto> AddEmailChannel(EmailChannelDto channelInput);

        /// <summary>
        /// Adds the email template to the database.
        /// </summary>
        /// <param name="templateInput"><see cref="EmailTemplateDto"/> model</param>
        /// <returns><see cref="EmailResponseDto{EmailTemplateDto}"/></returns>
        EmailResponseDto<EmailTemplateDto> AddEmailTemplate(EmailTemplateDto templateInput);

        /// <summary>
        /// Update the email template to the database.
        /// </summary>
        /// <param name="templateInput"><see cref="EmailTemplateDto"/> model</param>
        /// <returns><see cref="EmailResponseDto{EmailTemplateDto}"/></returns>
        EmailResponseDto<EmailTemplateDto> UpdateEmailTemplate(EmailTemplateDto templateInput);

        /// <summary>
        /// Gets the email channel by Key.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="EmailResponseDto{EmailChannelDto}"/></returns>
        EmailResponseDto<EmailChannelDto> GetEmailChannelByKey(string channelKey);

        /// <summary>
        /// Gets the email's history by tag and channel key.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="tag">Tag</param>
        /// <returns><<see cref="EmailResponseDto{List{EmailHistoryDto}}"/>/returns>
        EmailResponseDto<List<EmailHistoryDto>> GetEmailHistoriesByTag(string channelKey, string tag);

        /// <summary>
        /// Gets the emaik provider settings by pool name.
        /// </summary>
        /// <param name="poolName">Pool name</param>
        /// <param name="providerName">Provider name</param>
        /// <returns><see cref="EmailResponseDto{EmailProviderSettingsDto}"/></returns>
        EmailResponseDto<List<EmailProviderSettingsDto>> GetEmailProvidersByPool(string poolName, string providerName);
    }
}
