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

        public EmailResponseDto<EmailQuotaTable> UpdateEmailQuota(string channelID)
        {
            var response = new EmailResponseDto<EmailQuotaTable>();
            try
            {
                var emailQuota = _context.EmailQuotas.FirstOrDefault(q => q.EmailChannelID == channelID);
                if (emailQuota != null)
                {
                    var countInt = Convert.ToInt32(emailQuota.TotalConsumption); // TODO Implement encryption 
                    countInt += 1;
                    emailQuota.TotalConsumption = countInt;
                    _context.EmailQuotas.Update(emailQuota);
                }
                else
                {
                    emailQuota = new EmailQuotaTable()
                    {
                        EmailChannelID = channelID,
                        MonthlyQuota = 100,
                        MonthlyConsumption = 1,
                        TotalConsumption = 1
                    };
                    _context.EmailQuotas.Add(emailQuota);
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
    }
}
