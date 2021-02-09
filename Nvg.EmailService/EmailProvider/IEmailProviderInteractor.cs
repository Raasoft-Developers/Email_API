using Nvg.EmailService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.EmailProvider
{
    public interface IEmailProviderInteractor
    {

        /// <summary>
        /// Adds the email provider settings to the database.
        /// </summary>
        /// <param name="emailProviderInput"><see cref="EmailProviderSettingsDto"/></param>
        /// <returns><see cref="EmailResponseDto{EmailProviderSettingsDto}"/>/returns>
        EmailResponseDto<EmailProviderSettingsDto> AddEmailProvider(EmailProviderSettingsDto emailProviderInput);

        /// <summary>
        /// Gets the email provider settings by channel key.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="EmailResponseDto{EmailProviderSettingsDto}"/></returns>
        EmailResponseDto<EmailProviderSettingsDto> GetEmailProviderByChannel(string channelKey);

        /// <summary>
        /// Gets the email provider settings by pool name and provider name.
        /// </summary>
        /// <param name="poolName">Pool name</param>
        /// <param name="providerName">Provider name</param>
        /// <returns><see cref="EmailResponseDto{List{EmailProviderSettingsDto}}"/></returns>
        EmailResponseDto<List<EmailProviderSettingsDto>> GetEmailProvidersByPool(string poolName, string providerName);
    }
}
