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
                
        public EmailResponseDto<EmailChannelDto> GetEmailChannelByKey(string channelKey)
        {
            var response = new EmailResponseDto<EmailChannelDto>();
            try
            {
                var emailChannel = (from ec in _context.EmailChannels 
                                   from eq in _context.EmailQuotas.Where( quota => quota.EmailChannelID == ec.ID).DefaultIfEmpty()
                                   select new EmailChannelDto { 
                                   ID =ec.ID,
                                   Key = ec.Key,
                                   EmailPoolID = ec.EmailPoolID,
                                   EmailProviderID = ec.EmailProviderID,
                                   MonthlyQuota = eq.MonthlyQuota,
                                   TotalQuota = eq.TotalQuota,
                                   MonthlyConsumption = eq.MonthlyConsumption,
                                   TotalConsumption = eq.TotalConsumption,
                                   CurrentMonth = eq.CurrentMonth
                                   }).FirstOrDefault();
                //_context.EmailChannels.FirstOrDefault(sp => sp.Key.ToLower().Equals(channelKey.ToLower()));
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
