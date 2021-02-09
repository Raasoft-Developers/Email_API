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

                if (!string.IsNullOrEmpty(tag))
                    emailHistories = (from h in _context.EmailHistories
                                    join c in _context.EmailChannels on h.EmailChannelID equals c.ID
                                    where c.Key.ToLower().Equals(channelKey.ToLower()) && h.Tags.ToLower().Equals(tag.ToLower())
                                    select h).ToList();
                else
                    emailHistories = (from h in _context.EmailHistories
                                    join c in _context.EmailChannels on h.EmailChannelID equals c.ID
                                    where c.Key.ToLower().Equals(channelKey.ToLower())
                                    select h).ToList();

                if (emailHistories.Count != 0)
                    response.Status = true;
                else
                    response.Status = false;

                response.Message = $"Retrieved {emailHistories.Count} Email histories data for pool {tag}";
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
