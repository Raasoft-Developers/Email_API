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
        /// Updates the email quota.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="EmailResponseDto{EmailQuotaDto}"/></returns>
        EmailResponseDto<EmailQuotaDto> UpdateEmailQuota(string channelKey);
    }
}
