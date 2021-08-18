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
        EmailResponseDto<EmailChannelTable> AddEmailChannel(EmailChannelTable channelInput);

        /// <summary>
        /// Update the email channel in the database.
        /// </summary>
        /// <param name="channelInput"><see cref="EmailChannelTable"/> model</param>
        /// <returns><see cref="EmailResponseDto{T}"/> model</returns>
        EmailResponseDto<EmailChannelTable> UpdateEmailChannel(EmailChannelTable channelInput);

        /// <summary>
        /// Gets the Channel by Channel Key.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="EmailResponseDto{T}"/> model</returns>
        EmailResponseDto<EmailChannelDto> GetEmailChannelByKey(string channelKey);

        /// <summary>
        /// Checks if the channel exists.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="EmailResponseDto{T}"/> model</returns>
        EmailResponseDto<bool> CheckIfChannelExist(string channelKey);

        /// <summary>
        /// Gets the email channels in the database.
        /// </summary>
        /// <param name="poolID">Pool ID</param>
        /// <returns><see cref="EmailResponseDto{T}"/> model</returns>
        EmailResponseDto<List<EmailChannelDto>> GetEmailChannels(string poolID);

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

        /// <summary>
        /// Gets the email channel details by channel ID.
        /// </summary>
        /// <param name="channelID">Channel ID</param>
        /// <returns><see cref="EmailResponseDto{T}"/> model</returns>
        EmailResponseDto<EmailChannelTable> GetEmailChannelByID(string channelID);

        /// <summary>
        /// Checks if Channel ID is valid.
        /// </summary>
        /// <param name="channelID">channel ID</param>
        /// <returns><see cref="EmailResponseDto{T}"/> model</returns>
        EmailResponseDto<string> CheckIfEmailChannelIDIsValid(string channelID);

        /// <summary>
        /// Checks if Channel ID and key exists in the database.
        /// </summary>
        /// <param name="channelID">Channel ID</param>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="EmailResponseDto{T}"/> model</returns>
        EmailResponseDto<string> CheckIfEmailChannelIDKeyValid(string channelID, string channelKey);
    }
}
