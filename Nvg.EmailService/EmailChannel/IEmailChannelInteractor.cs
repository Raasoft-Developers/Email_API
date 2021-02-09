using Nvg.EmailService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.EmailChannel
{
    public interface IEmailChannelInteractor
    {
        /// <summary>
        /// Adds the email channel to the database.
        /// </summary>
        /// <param name="channelInput"><see cref="EmailChannelDto"/> model</param>
        /// <returns><see cref="EmailResponseDto{EmailChannelDto}"/></returns>
        EmailResponseDto<EmailChannelDto> AddEmailChannel(EmailChannelDto channelInput);

        /// <summary>
        /// /Gets the email channel by channel key.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="EmailResponseDto{EmailChannelDto}"/></returns>
        EmailResponseDto<EmailChannelDto> GetEmailChannelByKey(string channelKey);

        /// <summary>
        /// Checks if the channel exists for a given channel key.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="EmailResponseDto{bool}"/></returns>
        EmailResponseDto<bool> CheckIfChannelExist(string channelKey);
    }
}
