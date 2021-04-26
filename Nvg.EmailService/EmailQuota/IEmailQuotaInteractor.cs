using Nvg.EmailService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.EmailQuota
{
    public interface IEmailQuotaInteractor
    {
        /// <summary>
        /// Gets the Email quota by channel key.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="EmailResponseDto{EmailQuotaDto}"/></returns>
        EmailResponseDto<EmailQuotaDto> GetEmailQuota(string channelKey);
        
        /// <summary>
        /// Check if Email Quota of the channel is exceeded.Status is set to true or false based on value
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="bool"/></returns>
        bool CheckIfQuotaExceeded(string channelKey);


        /// <summary>
        /// Adds the Email Quota Values for Channel.
        /// </summary>
        /// <param name="emailChannel">Email Channel </param>
        /// <returns><see cref="EmailResponseDto{EmailQuotaDto}"/></returns>
        EmailResponseDto<EmailQuotaDto> AddEmailQuota(EmailChannelDto emailChannel);

        /// <summary>
        /// Updates the email quota.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="EmailResponseDto{EmailQuotaDto}"/></returns>
        EmailResponseDto<EmailQuotaDto> UpdateEmailQuota(string channelKey);
    }
}
