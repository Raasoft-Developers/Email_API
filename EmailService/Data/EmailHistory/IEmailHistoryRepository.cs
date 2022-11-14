using EmailService.Data.Entities;
using EmailService.DTOS;
using System.Collections.Generic;

namespace EmailService.Data.EmailHistory
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
        /// Gets the email histories by tag.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="tag">Tag</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<List<EmailHistoryTable>> GetEmailHistoriesByTag(string channelKey, string tag);

        /// <summary>
        /// Gets the Email history in between 2 date ranges
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="tag">Tag</param>
        /// <param name="fromDate">From Date</param>
        /// <param name="toDate">To Date</param>
        /// <returns><see cref="EmailResponseDto{T}"/> model</returns>
        EmailResponseDto<List<EmailHistoryTable>> GetEmailHistoriesByDateRange(string channelKey, string tag, string fromDate, string toDate);
    }
}