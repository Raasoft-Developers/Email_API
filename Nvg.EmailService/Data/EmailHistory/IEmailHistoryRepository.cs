using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;
using System.Collections.Generic;

namespace Nvg.EmailService.Data.EmailHistory
{
    public interface IEmailHistoryRepository
    {
        /// <summary>
        /// Adds the Email history into the database.
        /// </summary>
        /// <param name="historyInput"><see cref="EmailHistoryTable"/> model</param>
        /// <returns><see cref="EmailResponseDto{T}"/> model</returns>
        EmailResponseDto<EmailHistoryTable> AddEmailHistory(EmailHistoryTable historyInput);

        /// <summary>
        /// Gets the email's history by tag.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="tag">Tag</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<List<EmailHistoryTable>> GetEmailHistoriesByTag(string channelKey, string tag);
    }
}