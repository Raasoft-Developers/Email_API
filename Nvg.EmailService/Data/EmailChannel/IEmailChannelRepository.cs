using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;

namespace Nvg.EmailService.Data.EmailChannel
{
    public interface IEmailChannelRepository
    {
        /// <summary>
        /// Adds the email channel to the database.
        /// </summary>
        /// <param name="channelInput"><see cref="EmailChannelTable"/> model</param>
        /// <returns><see cref="EmailResponseDto{EmailChannelTable}"/> model</returns>
        EmailResponseDto<EmailChannelTable> AddEmailChannel(EmailChannelTable channelInput);

        /// <summary>
        /// Gets the Channel by Channel Key.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="EmailResponseDto{EmailChannelTable}"/> model</returns>
        EmailResponseDto<EmailChannelTable> GetEmailChannelByKey(string channelKey);

        /// <summary>
        /// Checks if the channel exists.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="EmailResponseDto{bool}"/> model</returns>
        EmailResponseDto<bool> CheckIfChannelExist(string channelKey);
    }
}
