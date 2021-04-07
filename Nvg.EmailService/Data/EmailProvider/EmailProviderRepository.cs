using Microsoft.Extensions.Logging;
using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nvg.EmailService.Data.EmailProvider
{
    public class EmailProviderRepository : IEmailProviderRepository
    {
        private readonly EmailDbContext _context;
        private readonly ILogger<IEmailProviderRepository> _logger;

        public EmailProviderRepository(EmailDbContext context, ILogger<IEmailProviderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public EmailResponseDto<EmailProviderSettingsTable> AddEmailProvider(EmailProviderSettingsTable providerInput)
        {
            var response = new EmailResponseDto<EmailProviderSettingsTable>();
            try
            {
                var provider = _context.EmailProviders.FirstOrDefault(sp => sp.Name.Equals(providerInput.Name) && sp.EmailPoolID.Equals(providerInput.EmailPoolID) && sp.Type.Equals(providerInput.Type));
                if (provider != null)
                {
                    response.Status = false;
                    response.Message = "The Provider already exists.";
                    return response;
                }
                else
                {
                    providerInput.ID = Guid.NewGuid().ToString();
                    _context.EmailProviders.Add(providerInput);
                    if (_context.SaveChanges() == 1)
                    {
                        response.Status = true;
                        response.Message = "Added";
                        response.Result = providerInput;
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Not Added";
                        response.Result = providerInput;
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

        public EmailResponseDto<EmailProviderSettingsTable> UpdateEmailProvider(EmailProviderSettingsTable providerInput)
        {
            var response = new EmailResponseDto<EmailProviderSettingsTable>();
            try
            {
                var provider = _context.EmailProviders.FirstOrDefault(sp => sp.Name.Equals(providerInput.Name) && sp.EmailPoolID.Equals(providerInput.EmailPoolID) && sp.Type.Equals(providerInput.Type));
                if (provider != null)
                {
                    provider.Configuration = providerInput.Configuration;
                    if (_context.SaveChanges() == 1)
                    {
                        response.Status = true;
                        response.Message = "Updated";
                        response.Result = providerInput;
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Failed To Update";
                        response.Result = providerInput;
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Cannot find Provider with Name {providerInput.Name}";
                    return response;
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

        public EmailResponseDto<EmailProviderSettingsTable> GetEmailProviderByName(string providerName)
        {
            var response = new EmailResponseDto<EmailProviderSettingsTable>();
            try
            {
                var emailProvider = _context.EmailProviders.FirstOrDefault(sp => sp.Name.ToLower().Equals(providerName.ToLower()));
                if (emailProvider != null)
                {
                    response.Status = true;
                    response.Message = $"Retrieved Email provider data for {providerName}";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Email provider data for {providerName} is not available";
                }
                response.Result = emailProvider;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public EmailResponseDto<EmailProviderSettingsTable> GetEmailProviderByChannelKey(string channelKey)
        {
            _logger.LogInformation("GetEmailProviderByChannelKey");
            var response = new EmailResponseDto<EmailProviderSettingsTable>();
            try
            {
                var emailProvider = (from p in _context.EmailProviders
                                     join c in _context.EmailChannels on new { PoolID = p.EmailPoolID, ProviderID = p.ID } equals new { PoolID = c.EmailPoolID, ProviderID = c.EmailProviderID }
                                     where c.Key.ToLower().Equals(channelKey.ToLower())
                                     select p).FirstOrDefault();
                if (emailProvider != null)
                {
                    response.Status = true;
                    response.Message = $"Retrieved Email provider data for channel {channelKey}";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Email provider data for channel {channelKey} is not available.";
                }
                _logger.LogDebug($"Status : {response.Status}");
                _logger.LogDebug($"Message : {response.Message}");

                response.Result = emailProvider;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public EmailResponseDto<List<EmailProviderSettingsTable>> GetEmailProvidersByPool(string poolName, string providerName)
        {
            var response = new EmailResponseDto<List<EmailProviderSettingsTable>>();
            try
            {
                var emailProviders = (from p in _context.EmailProviders
                                    join sp in _context.EmailPools on p.EmailPoolID equals sp.ID
                                    where sp.Name.ToLower().Equals(poolName.ToLower())
                                    select p).ToList();

                if (emailProviders.Count != 0)
                {
                    if (!string.IsNullOrEmpty(providerName))
                        emailProviders = emailProviders.Where(s => s.Name.ToLower().Equals(providerName.ToLower())).ToList();
                    response.Status = true;
                }
                else
                    response.Status = false;

                response.Message = $"Retrieved {emailProviders.Count} Email providers data for pool {poolName}";
                response.Result = emailProviders;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public EmailResponseDto<string> CheckIfEmailProviderIDIsValid(string providerID)
        {
            var response = new EmailResponseDto<string>();
            try
            {
                var smsPool = _context.EmailProviders.Any(sp => sp.ID.ToLower().Equals(providerID.ToLower()));
                if (smsPool)
                {
                    response.Status = true;
                    response.Message = $"Email Provider ID is valid.";
                    response.Result = "Valid Email Provider.";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Email Provider data is not available";
                    response.Result = "Invalid Email Provider.";
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

        public EmailResponseDto<string> CheckIfEmailProviderIDNameValid(string providerID, string providerName)
        {
            var response = new EmailResponseDto<string>();
            try
            {
                var smsPool = _context.EmailProviders.Any(sp => sp.ID.ToLower().Equals(providerID.ToLower()) && sp.Name.ToLower().Equals(providerName.ToLower()));
                if (smsPool)
                {
                    response.Status = true;
                    response.Message = $"Valid Provider ID and Provider Name {providerName}.";
                    response.Result = "Email Provider Valid.";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Invalid Provider ID and Provider Name {providerName}";
                    response.Result = "Email Provider Invalid.";
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
