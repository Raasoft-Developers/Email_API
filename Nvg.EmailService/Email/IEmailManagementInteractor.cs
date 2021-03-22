using Nvg.EmailService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.Email
{
    public interface IEmailManagementInteractor
    {
        #region Email Pool
        /// <summary>
        /// Gets all the email pools.
        /// </summary>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<List<EmailPoolDto>> GetEmailPools();

        /// <summary>
        /// Gets all the email pool Names.
        /// </summary>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<List<EmailPoolDto>> GetEmailPoolNames();

        /// <summary>
        /// Updates the email pool into the database.
        /// </summary>
        /// <param name="emailPoolInput"><see cref="EmailPoolDto"/> model</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<EmailPoolDto> UpdateEmailPool(EmailPoolDto emailPoolInput);

        /// <summary>
        /// Delete the email pool into the database.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<string> DeleteEmailPool(string poolID);
        #endregion

        #region Email Provider
        /// <summary>
        /// Gets all the email Provider.
        /// </summary>
        /// <param name="poolName">Pool Name</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<List<EmailProviderSettingsDto>> GetEmailProviders(string poolName);

        /// <summary>
        /// Gets all the email Provider Names.
        /// </summary>
        /// <param name="poolName">Pool Name</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<List<EmailProviderSettingsDto>> GetEmailProviderNames(string poolName);

        /// <summary>
        /// Add/Update the email provider to the database.
        /// </summary>
        /// <param name="providerInput"><see cref="EmailProviderSettingsDto"/></param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<EmailProviderSettingsDto> AddUpdateEmailProvider(EmailProviderSettingsDto providerInput);

        /// <summary>
        /// Delete the Email Provider into the database.
        /// </summary>
        /// <param name="providerID">Provider ID</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<string> DeleteEmailProvider(string providerID);
        #endregion

        #region Email Channel
        /// <summary>
        /// Gets all the email Channels for pool.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<List<EmailChannelDto>> GetEmailChannelsByPool(string poolID);

        /// <summary>
        /// Delete the Email Channel into the database.
        /// </summary>
        /// <param name="channelID">Provider ID</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<string> DeleteEmailChannel(string channelID);
        /// <summary>
        /// Add/Update the email provider to the database.
        /// </summary>
        /// <param name="channelInput"><see cref="EmailChannelDto"/></param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<EmailChannelDto> AddUpdateEmailChannel(EmailChannelDto channelInput);

        /// <summary>
        /// Gets the Email Channel keys from the database.
        /// </summary>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<List<EmailChannelDto>> GetEmailChannelKeys();
        #endregion

        #region Email Template
        /// <summary>
        /// Gets all the email Templates for pool.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<List<EmailTemplateDto>> GetEmailTemplatesByPool(string poolID);

        /// <summary>
        /// Gets all the email Templates for pool.
        /// </summary>
        /// <param name="channelID">Channel ID</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<List<EmailTemplateDto>> GetEmailTemplatesByChannelID(string channelID);

        /// <summary>
        /// Add/Updates the email template
        /// </summary>
        /// <param name="templateInput"><see cref="EmailTemplateDto"/></param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<EmailTemplateDto> AddUpdateEmailTemplate(EmailTemplateDto templateInput);

        /// <summary>
        /// Delete the Email Template into the database.
        /// </summary>
        /// <param name="templateID">Template ID</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<string> DeleteEmailTemplate(string templateID);
        #endregion

        #region Email Histories

        EmailResponseDto<List<EmailHistoryDto>> GetEmailHistories(string channelID, string tag);
        #endregion
    }
}
