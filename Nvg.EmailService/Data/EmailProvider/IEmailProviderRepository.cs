using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.Data.EmailProvider
{
    public interface IEmailProviderRepository
    {
        /// <summary>
        /// Adds the email provider to the database.
        /// </summary>
        /// <param name="providerInput"><see cref="EmailProviderSettingsTable"/> model</param>
        /// <returns><see cref="EmailResponseDto{EmailProviderSettingsTable}"/></returns>
        EmailResponseDto<EmailProviderSettingsTable> AddEmailProvider(EmailProviderSettingsTable providerInput);

        /// <summary>
        /// Gets the email provider setttings by provider name.
        /// </summary>
        /// <param name="providerName">Provider Name</param>
        /// <returns><see cref="EmailResponseDto{EmailProviderSettingsTable}"/></returns>
        EmailResponseDto<EmailProviderSettingsTable> GetEmailProviderByName(string providerName);

        /// <summary>
        /// Gets the email provider setttings by channel key.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="EmailResponseDto{EmailProviderSettingsTable}"/></returns>
        EmailResponseDto<EmailProviderSettingsTable> GetEmailProviderByChannelKey(string channelKey);

        /// <summary>
        /// Gets the email provider settings by pool name and provider name.
        /// </summary>
        /// <param name="poolName">Pool name</param>
        /// <param name="providerName">Provider Name</param>
        /// <returns><see cref="EmailResponseDto{List{EmailProviderSettingsTable}}"/></returns>
        EmailResponseDto<List<EmailProviderSettingsTable>> GetEmailProvidersByPool(string poolName, string providerName);
    }
}
