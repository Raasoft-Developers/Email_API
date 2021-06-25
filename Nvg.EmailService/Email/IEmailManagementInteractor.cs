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
        /// Adds the email pool to database.
        /// </summary>
        /// <param name="poolInput"><see cref="EmailPoolDto"/> model</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<EmailPoolDto> AddEmailPool(EmailPoolDto poolInput);

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
        /// Adds the email provider to the database.
        /// </summary>
        /// <param name="providerInput"><see cref="EmailProviderSettingsDto"/></param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<EmailProviderSettingsDto> AddEmailProvider(EmailProviderSettingsDto providerInput);

        /// <summary>
        /// Updates the email provider to the database.
        /// </summary>
        /// <param name="providerInput"><see cref="EmailProviderSettingsDto"/></param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<EmailProviderSettingsDto> UpdateEmailProvider(EmailProviderSettingsDto providerInput);

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
        /// Adds the email provider to the database.
        /// </summary>
        /// <param name="channelInput"><see cref="EmailChannelDto"/></param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<EmailChannelDto> AddEmailChannel(EmailChannelDto channelInput);

        /// <summary>
        /// Updates the email provider to the database.
        /// </summary>
        /// <param name="channelInput"><see cref="EmailChannelDto"/></param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<EmailChannelDto> UpdateEmailChannel(EmailChannelDto channelInput);

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
        /// Add the email template.
        /// </summary>
        /// <param name="templateInput"><see cref="EmailTemplateDto"/></param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<EmailTemplateDto> AddEmailTemplate(EmailTemplateDto templateInput);

        /// <summary>
        /// Updates the email template.
        /// </summary>
        /// <param name="templateInput"><see cref="EmailTemplateDto"/></param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<EmailTemplateDto> UpdateEmailTemplate(EmailTemplateDto templateInput);

        /// <summary>
        /// Gets email template by id
        /// </summary>
        /// <param name="templateID"></param>
        /// <returns></returns>
        EmailResponseDto<EmailTemplateDto> GetEmailTemplate(string templateID);

        /// <summary>
        /// Delete the Email Template into the database.
        /// </summary>
        /// <param name="templateID">Template ID</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<string> DeleteEmailTemplate(string templateID);
        #endregion

        #region Email Histories
        /// <summary>
        /// Get the email histories by Channel.
        /// </summary>
        /// <param name="channelID">Channel ID</param>
        /// <param name="tag">Tag</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<List<EmailHistoryDto>> GetEmailHistories(string channelID, string tag);
        #endregion


        /// <summary>
        /// Method to send an email.
        /// </summary>
        /// <param name="emailInputs"><see cref="EmailDto"/> model</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<string> SendMail(EmailDto emailInputs);


    }
}
