using EmailService.Data.Entities;
using EmailService.DTOS;
using System.Collections.Generic;

namespace EmailService.Data.EmailQuota
{
    public interface IEmailQuotaRepository
    {
        /// <summary>
        /// Gets the Email Quota by channel key.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<EmailQuotaTable> GetEmailQuota(string channelKey);

        /// <summary>
        /// Increments the Consumption value in Email Quota .
        /// </summary>
        /// <param name="channelID">Channel Id</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<EmailQuotaTable> IncrementEmailQuota(string channelID);

        /// <summary>
        /// Updates the Email Quota.
        /// </summary>
        /// <param name="emailChannel">Email Channel </param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<EmailQuotaTable> UpdateEmailQuota(EmailChannelDto emailChannel);
        /// <summary>
        /// Adds the Email Quota Values for Channel.
        /// </summary>
        /// <param name="emailChannel">Email Channel </param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<EmailQuotaTable> AddEmailQuota(EmailChannelDto emailChannel);


        /// <summary>
        /// Updates the Current Month of the Channel's Quota.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="currentMonth">Current Month</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<EmailQuotaTable> UpdateCurrentMonth(string channelKey, string currentMonth);

        /// <summary>
        /// Deletes the  Email Values from the Email Channel ID.
        /// </summary>
        /// <param name="channelID">Channel Id</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<string> DeleteEmailQuota(string channelID);

        /// <summary>
        /// Gets the Email Quota by channel ID.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<List<EmailQuotaTable>> GetEmailQuotaList(string channelID);

    }
}
