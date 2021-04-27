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
                    response.Status = false;
                    response.Message = "This Channel already exists.";
                    response.Result = channelInput;
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

        public EmailResponseDto<EmailChannelTable> UpdateEmailChannel(EmailChannelTable channelInput)
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
                    response.Status = false;
                    response.Message = $"Cannot find Channel with Key {channelInput.Key}";
                    response.Result = channelInput;
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
        /// Gets the Channel by Channel ID.
        /// </summary>
        /// <param name="channelID">Channel Key</param>
        /// <returns><see cref="EmailResponseDto{EmailChannelTable}"/> model</returns>
        public EmailResponseDto<EmailChannelTable> GetEmailChannelByID(string channelID)
        {
            var response = new EmailResponseDto<EmailChannelTable>();
            try
            {
                var emailChannel = _context.EmailChannels.FirstOrDefault(sp => sp.ID.ToLower().Equals(channelID.ToLower()));
                if (emailChannel != null)
                {
                    response.Message = $"Retrieved Email channel data";
                }
                else
                {
                    response.Message = $"Email Channel Data Unavailable";
                }
                response.Status = true;
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

        public EmailResponseDto<List<EmailChannelTable>> GetEmailChannels(string poolID)
        {
            var response = new EmailResponseDto<List<EmailChannelTable>>();
            try
            {
                var emailChannels = (from p in _context.EmailPools
                                    join c in _context.EmailChannels on p.ID equals c.EmailPoolID
                                    join pr in _context.EmailProviders on c.EmailProviderID equals pr.ID
                                    where p.ID.ToLower().Equals(poolID.ToLower())
                                    select new EmailChannelTable
                                    {
                                        ID=c.ID,
                                        Key=c.Key,
                                        EmailPoolID=c.EmailPoolID,
                                        EmailPoolName=p.Name,
                                        EmailProviderID=c.EmailProviderID,
                                        EmailProviderName=pr.Name
                                    }).ToList();
                
                response.Status = true;
                response.Message = $"Retrieved {emailChannels.Count} Email channel data";                
                response.Result = emailChannels;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public EmailResponseDto<string> DeleteEmailChannel(string channelID)
        {
            var response = new EmailResponseDto<string>();
            try
            {
                var emailChannel = _context.EmailChannels.Where(o => o.ID.ToLower().Equals(channelID.ToLower())).FirstOrDefault();
                if (emailChannel != null)
                {
                    _context.EmailChannels.Remove(emailChannel);
                    if (_context.SaveChanges() == 1)
                    {
                        response.Status = true;
                        response.Message = $"Deleted Successfully";
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = $"Failed to delete";
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Email Channel Data not found";
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

        public EmailResponseDto<List<EmailChannelTable>> GetEmailChannelKeys()
        {
            var response = new EmailResponseDto<List<EmailChannelTable>>();
            try
            {
                var emailChannelKeys = _context.EmailChannels.Select(o => new EmailChannelTable { Key = o.Key, ID = o.ID }).ToList();
               
                response.Status = true;
                response.Message = $"Retrieved {emailChannelKeys.Count} keys";                
                response.Result = emailChannelKeys;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public EmailResponseDto<string> CheckIfEmailChannelIDIsValid(string channelID)
        {
            var response = new EmailResponseDto<string>();
            try
            {
                var smsPool = _context.EmailChannels.Any(sp => sp.ID.ToLower().Equals(channelID.ToLower()));
                if (smsPool)
                {
                    response.Status = true;
                    response.Message = $"Email Channel ID is valid.";
                    response.Result = "Valid Email Channel.";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Email Channel data is not available";
                    response.Result = "Invalid Email Channel.";
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

        public EmailResponseDto<string> CheckIfEmailChannelIDKeyValid(string channelID, string channelKey)
        {
            var response = new EmailResponseDto<string>();
            try
            {
                var smsPool = _context.EmailChannels.Any(sp => sp.ID.ToLower().Equals(channelID.ToLower()) && sp.Key.ToLower().Equals(channelKey.ToLower()));
                if (smsPool)
                {
                    response.Status = true;
                    response.Message = $"Valid Channel ID and Channel Key {channelKey}.";
                    response.Result = "Email Channel Valid.";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Invalid Channel ID and Channel Key {channelKey}";
                    response.Result = "Email Channel Invalid.";
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
    }
}
