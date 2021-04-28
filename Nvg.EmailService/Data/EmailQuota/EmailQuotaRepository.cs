using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nvg.EmailService.Data.EmailQuota
{
    public class EmailQuotaRepository : IEmailQuotaRepository
    {
        private readonly EmailDbContext _context;

        public EmailQuotaRepository(EmailDbContext context)
        {
            _context = context;
        }

        public EmailResponseDto<EmailQuotaTable> GetEmailQuota(string channelKey)
        {
            var response = new EmailResponseDto<EmailQuotaTable>();
            try
            {
                var emailQuota = (from q in _context.EmailQuotas
                                join c in _context.EmailChannels on q.EmailChannelID equals c.ID
                                where c.Key.ToLower().Equals(channelKey.ToLower())
                                select q).FirstOrDefault();
                response.Status = true;
                response.Message = $"Retrieved Email Quota";
                response.Result = emailQuota;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public EmailResponseDto<EmailQuotaTable> UpdateCurrentMonth(string channelKey,string currentMonth)
        {
            var response = new EmailResponseDto<EmailQuotaTable>();
            try
            {
                var emailQuota = (from q in _context.EmailQuotas
                                  join c in _context.EmailChannels on q.EmailChannelID equals c.ID
                                  where c.Key.ToLower().Equals(channelKey.ToLower())
                                  select q).FirstOrDefault();
                emailQuota.CurrentMonth = currentMonth;
                emailQuota.MonthlyConsumption = 0;
                var updated = _context.SaveChanges();
                if(updated>0)
                {
                    response.Status = true;
                    response.Message = $"Updated Channel Quota Current Month";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Failed to Update Channel Quota Current Month";
                }
                response.Result = emailQuota;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }
        public EmailResponseDto<EmailQuotaTable> IncrementEmailQuota(string channelID)
        {
            var response = new EmailResponseDto<EmailQuotaTable>();
            try
            {
                var emailQuota = _context.EmailQuotas.FirstOrDefault(q => q.EmailChannelID == channelID);
                if (emailQuota != null)
                {
                    var totalCountInt = Convert.ToInt32(emailQuota.TotalConsumption); // TODO Implement encryption 
                    var monthCountInt = Convert.ToInt32(emailQuota.TotalConsumption); // TODO Implement encryption 
                    monthCountInt += 1; totalCountInt += 1;
                    emailQuota.MonthlyConsumption += 1;
                    emailQuota.TotalConsumption = totalCountInt;
                    _context.EmailQuotas.Update(emailQuota);
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Email Quota does not exist for Channel ID {channelID}";
                    response.Result = emailQuota;
                }
                if (_context.SaveChanges() == 1)
                {
                    response.Status = true;
                    response.Message = "Email Quota is updated";
                    response.Result = emailQuota;
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
        public EmailResponseDto<EmailQuotaTable> AddEmailQuota(EmailChannelDto emailChannel)
        {
            var response = new EmailResponseDto<EmailQuotaTable>();
            try
            {
                var emailQuota = _context.EmailQuotas.FirstOrDefault(q => q.EmailChannelID == emailChannel.ID);
                if (emailQuota == null)
                {
                    emailQuota = new EmailQuotaTable()
                    {
                        EmailChannelID = emailChannel.ID,
                        MonthlyQuota =  emailChannel.MonthlyQuota ,
                        MonthlyConsumption = 0,
                        TotalConsumption = 0,
                        TotalQuota =  emailChannel.TotalQuota ,
                        CurrentMonth = DateTime.Now.ToString("MMM").ToUpper()
                    };
                    _context.EmailQuotas.Add(emailQuota);
                }
                else
                {
                    response.Status = false;
                    response.Message = "Email Quota already exists";
                    response.Result = emailQuota;
                }
                if (_context.SaveChanges() == 1)
                {
                    response.Status = true;
                    response.Message = "Email Quota is Added";
                    response.Result = emailQuota;
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

        public EmailResponseDto<EmailQuotaTable> UpdateEmailQuota(EmailChannelDto emailChannel)
        {
            var response = new EmailResponseDto<EmailQuotaTable>();
            try
            {
                var emailQuota = _context.EmailQuotas.FirstOrDefault(q => q.EmailChannelID == emailChannel.ID);
                if (emailQuota != null)
                {
                    if (emailChannel.IsRestrictedByQuota)
                    {
                        emailQuota.TotalQuota = emailChannel.TotalQuota;
                        emailQuota.MonthlyQuota =emailChannel.MonthlyQuota;
                    }
                    else
                    {
                        _context.EmailQuotas.Remove(emailQuota);
                    }
                }
                else
                {
                    if (emailChannel.IsRestrictedByQuota)
                    {
                        emailQuota = new EmailQuotaTable()
                        {
                            EmailChannelID = emailChannel.ID,
                            MonthlyQuota =  emailChannel.MonthlyQuota ,
                            MonthlyConsumption = 0,
                            TotalConsumption = 0,
                            TotalQuota = emailChannel.TotalQuota ,
                            CurrentMonth = DateTime.Now.ToString("MMM").ToUpper()
                        };
                        _context.EmailQuotas.Add(emailQuota);
                    }
                }
                if (_context.SaveChanges() > 0)
                {
                    response.Status = true;
                    response.Message = "Email Quota is Updated";
                    response.Result = emailQuota;
                }
                else
                {
                    response.Status = false;
                    response.Message = "Email Quota is not Updated";
                    response.Result = emailQuota;
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
        public EmailResponseDto<string> DeleteEmailQuota(string channelID)
        {
            var response = new EmailResponseDto<string>();
            try
            {
                var emailQuota = _context.EmailQuotas.Where(q => q.EmailChannelID.ToLower().Equals(channelID.ToLower())).FirstOrDefault();
                if (emailQuota != null)
                {
                    _context.EmailQuotas.Remove(emailQuota);
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
                    response.Message = $"Email Quota Data not found";
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
