using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nvg.EmailService.Data.EmailChannel
{
    public class EmailChannelRepository : IEmailChannelRepository
    {
        private readonly EmailDbContext _context;

        public EmailChannelRepository(EmailDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds the email channel to the database.
        /// </summary>
        /// <param name="channelInput"><see cref="EmailChannelTable"/> model</param>
        /// <returns><see cref="EmailResponseDto{EmailChannelTable}"/> model</returns>
        public EmailResponseDto<EmailChannelTable> AddEmailChannel(EmailChannelTable channelInput)
        {
            var response = new EmailResponseDto<EmailChannelTable>();
            try
            {
                var channel = _context.EmailChannels.FirstOrDefault(sp => sp.Key.Equals(channelInput.Key) && sp.EmailPoolID.Equals(channelInput.EmailPoolID));
                if (channel != null)
                {
                    channel.EmailProviderID = channelInput.EmailProviderID;
                    if (_context.SaveChanges() == 1)
                    {
                        response.Status = true;
                        response.Message = "Updated";
                        response.Result = channelInput;
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Failed To Update";
                        response.Result = channelInput;
                    }
                }
                else
                {
                    channelInput.ID = Guid.NewGuid().ToString();
                    _context.EmailChannels.Add(channelInput);
                    if (_context.SaveChanges() == 1)
                    {
                        response.Status = true;
                        response.Message = "Added";
                        response.Result = channelInput;
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Not Added";
                        response.Result = channelInput;
                    }
                }
                
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        /// <summary>
        /// Gets the Channel by Channel Key.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="EmailResponseDto{EmailChannelTable}"/> model</returns>
        public EmailResponseDto<EmailChannelTable> GetEmailChannelByKey(string channelKey)
        {
            var response = new EmailResponseDto<EmailChannelTable>();
            try
            {
                var emailChannel = _context.EmailChannels.FirstOrDefault(sp => sp.Key.ToLower().Equals(channelKey.ToLower()));
                if (emailChannel != null)
                {
                    response.Status = true;
                    response.Message = $"Retrieved Email channel data for {channelKey}";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Email Channel Data Unavailable for {channelKey}";
                }
                response.Result = emailChannel;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        /// <summary>
        /// Checks if the channel exists.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <returns><see cref="EmailResponseDto{bool}"/> model</returns>
        public EmailResponseDto<bool> CheckIfChannelExist(string channelKey)
        {
            var response = new EmailResponseDto<bool>();
            try
            {
                var channelExist = _context.EmailChannels.Any(sp => sp.Key.ToLower().Equals(channelKey.ToLower()));
                response.Status = channelExist;
                response.Message = $"Is channel existing : {channelExist}";
                response.Result = channelExist;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                response.Result = false;
                return response;
            }
        }
    }
}
