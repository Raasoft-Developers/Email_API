using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;
using System.Collections.Generic;

namespace Nvg.EmailService.Data.EmailChannel
{
    public interface IEmailChannelRepository
    {
        /// <summary>
        /// Adds/Updates the email channel to the database.
        /// </summary>
        /// <param name="channelInput"><see cref="EmailChannelTable"/> model</param>
        /// <returns><see cref="EmailResponseDto{T}"/> model</returns>
        EmailResponseDto<EmailChannelTable> AddUpdateEmailChannel(EmailChannelTable channelInput);

        /// <summary>
        /// Gets the Channel by Channel Key.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="EmailResponseDto{T}"/> model</returns>
        EmailResponseDto<EmailChannelTable> GetEmailChannelByKey(string channelKey);

        /// <summary>
        /// Checks if the channel exists.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="EmailResponseDto{bool}"/> model</returns>
        EmailResponseDto<bool> CheckIfChannelExist(string channelKey);

        /// <summary>
        /// Gets the email channels in the database.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="EmailResponseDto{T}"/> model</returns>
        EmailResponseDto<List<EmailChannelTable>> GetEmailChannels(string poolID);

        /// <summary>
        /// Delete the email channel in the database.
        /// </summary>
        /// <param name="channelID">Channel ID</param>
        /// <returns><see cref="EmailResponseDto{T}"/> model</returns>
        EmailResponseDto<string> DeleteEmailChannel(string channelID);

        /// <summary>
        /// Gets the email channel keys in the database.
        /// </summary>
        /// <returns><see cref="EmailResponseDto{T}"/> model</returns>
        EmailResponseDto<List<EmailChannelTable>> GetEmailChannelKeys();

        EmailResponseDto<EmailChannelTable> GetEmailChannelByID(string channelID);
    }
}
