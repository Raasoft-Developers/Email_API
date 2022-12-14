using EmailService.DTOS;
using System.Collections.Generic;

namespace EmailService.EmailHistory
{
    public interface IEmailHistoryInteractor
    {
        /// <summary>
        /// Adds the email history to the database.
        /// </summary>
        /// <param name="historyInput"><see cref="EmailHistoryDto"/></param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<EmailHistoryDto> AddEmailHistory(EmailHistoryDto historyInput);

        /// <summary>
        /// Gets the email's history by tag.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="tag">Tag</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<List<EmailHistoryDto>> GetEmailHistoriesByTag(string channelKey, string tag);

        /// <summary>
        /// Gets the Email History data by date range.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="tag">Tag</param>
        /// <param name="fromDate">From date</param>
        /// <param name="toDate">To Date</param>
        /// <returns><see cref="EmailResponseDto{T}"/> model</returns>
        EmailResponseDto<List<EmailHistoryDto>> GetEmailHistoriesByDateRange(string channelKey, string tag, string fromDate, string toDate);
    }
}