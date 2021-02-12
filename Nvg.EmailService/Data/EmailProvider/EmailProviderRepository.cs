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

        public EmailProviderRepository(EmailDbContext context)
        {
            _context = context;
        }

        public EmailResponseDto<EmailProviderSettingsTable> AddEmailProvider(EmailProviderSettingsTable providerInput)
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
            var response = new EmailResponseDto<EmailProviderSettingsTable>();
            try
            {
                var emailProvider = (from p in _context.EmailProviders
                                   join c in _context.EmailChannels on p.EmailPoolID equals c.EmailPoolID
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
    }
}
