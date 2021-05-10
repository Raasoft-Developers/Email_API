using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.Data.EmailQuota
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
        /// Updates the Email Quota.
        /// </summary>
        /// <param name="channelID">Channel Id</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<EmailQuotaTable> UpdateEmailQuota(string channelID);
    }
}
