using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nvg.EmailService.Data.EmailHistory
{
    public class EmailHistoryRepository : IEmailHistoryRepository
    {
        private readonly EmailDbContext _context;

        public EmailHistoryRepository(EmailDbContext context)
        {
            _context = context;
        }

        public EmailResponseDto<EmailHistoryTable> AddEmailHistory(EmailHistoryTable historyInput)
        {
            var response = new EmailResponseDto<EmailHistoryTable>();
            try
            {
                historyInput.ID = Guid.NewGuid().ToString();
                historyInput = _context.EmailHistories.Add(historyInput).Entity;
                if (_context.SaveChanges() == 1)
                {
                    response.Status = true;
                    response.Message = "Added";
                    response.Result = historyInput;
                }
                else
                {
                    response.Status = false;
                    response.Message = "Not Added";
                    response.Result = historyInput;
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

        public EmailResponseDto<List<EmailHistoryTable>> GetEmailHistoriesByTag(string channelKey, string tag)
        {
            var response = new EmailResponseDto<List<EmailHistoryTable>>();
            try
            {
                var emailHistories = new List<EmailHistoryTable>();

                    emailHistories = (from h in _context.EmailHistories
                                    join c in _context.EmailChannels on h.EmailChannelID equals c.ID
                                    join pr in _context.EmailProviders on h.EmailProviderID equals pr.ID
                                    where c.Key.ToLower().Equals(channelKey.ToLower()) && (string.IsNullOrEmpty(tag) || h.Tags.ToLower().Equals(tag.ToLower()))
                                    select new EmailHistoryTable { 
                                    ID=h.ID,
                                    Sender=h.Sender,
                                    SentOn=h.SentOn,
                                    Recipients=h.Recipients,
                                    Attempts=h.Attempts,
                                    Tags=h.Tags,
                                    Status=h.Status,
                                    ChannelKey=c.Key,
                                    ProviderName=pr.Name,
                                    MessageSent=h.MessageSent,
                                    EmailChannelID=h.EmailChannelID,
                                    EmailProviderID=h.EmailProviderID,
                                    TemplateName=h.TemplateName,
                                    TemplateVariant=h.TemplateVariant
                                    }).ToList();

                if (emailHistories.Count != 0)
                    response.Status = true;
                else
                    response.Status = false;

                response.Message = $"Retrieved {emailHistories.Count} Email histories data for pool";
                response.Result = emailHistories;
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
