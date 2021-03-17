using Microsoft.Extensions.Logging;
using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nvg.EmailService.Data.EmailTemplate
{
    public class EmailTemplateRepository : IEmailTemplateRepository
    {
        private readonly EmailDbContext _context;
        private readonly ILogger<EmailTemplateRepository> _logger;

        public EmailTemplateRepository(EmailDbContext context, ILogger<EmailTemplateRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public EmailResponseDto<EmailTemplateTable> AddUpdateEmailTemplate(EmailTemplateTable templateInput)
        {
            var response = new EmailResponseDto<EmailTemplateTable>();
            try
            {
                var template = _context.EmailTemplates.FirstOrDefault(st => st.Name.ToLower().Equals(templateInput.Name.ToLower()) &&
                st.EmailPoolID.Equals(templateInput.EmailPoolID) &&
                st.Variant.ToLower().Equals(templateInput.Variant.ToLower()));
                if (template != null)
                {
                    template.MessageTemplate = templateInput.MessageTemplate;
                    template.Sender = templateInput.Sender;
                    if (_context.SaveChanges() == 1)
                    {
                        response.Status = true;
                        response.Message = "Updated";
                        response.Result = templateInput;
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Failed To Update";
                        response.Result = templateInput;
                    }
                }
                else
                {
                    templateInput.ID = Guid.NewGuid().ToString();
                    _context.EmailTemplates.Add(templateInput);
                    if (_context.SaveChanges() == 1)
                    {
                        response.Status = true;
                        response.Message = "Added";
                        response.Result = templateInput;
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Not Added";
                        response.Result = templateInput;
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

        public EmailResponseDto<bool> CheckIfTemplateExist(string channelKey, string templateName)
        {
            var response = new EmailResponseDto<bool>();
            try
            {
                var templateExist = (from t in _context.EmailTemplates
                                     join c in _context.EmailChannels on t.EmailPoolID equals c.EmailPoolID
                                     where t.Name.Equals(templateName) && c.Key.ToLower().Equals(channelKey.ToLower())
                                     select t).Any();
                response.Status = templateExist;
                response.Message = $"Is template existing : {templateExist}";
                response.Result = templateExist;
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

        public EmailTemplateTable GetEmailTemplate(string templateID)
        {
            return _context.EmailTemplates.FirstOrDefault(x => x.ID == templateID);
        }

        public EmailTemplateTable GetEmailTemplate(string templateName, string channelKey, string variant = null)
        {
            var emailTemplate = new EmailTemplateTable();
            var emailQry = (from t in _context.EmailTemplates
                          join c in _context.EmailChannels on t.EmailPoolID equals c.EmailPoolID
                          where c.Key.ToLower().Equals(channelKey.ToLower()) &&
                          (t.Name.Equals(templateName) && string.IsNullOrEmpty(variant) || !string.IsNullOrEmpty(variant) && t.Name.Equals(templateName) && t.Variant.ToLower().Equals(variant.ToLower()))
                          select t).ToList();

            if (!string.IsNullOrEmpty(variant))
                emailTemplate = emailQry.FirstOrDefault(st => !string.IsNullOrEmpty(st.Variant));
            else
                emailTemplate = emailQry.FirstOrDefault();

            /*if (smsTemplate == null)
                smsTemplate = _context.SMSTemplate.FirstOrDefault(t => t.Name == defaultTemplate);*/

            _logger.LogDebug($"Template used : {emailTemplate?.Name}");

            return emailTemplate;
        }

        public EmailResponseDto<List<EmailTemplateTable>> GetEmailTemplatesByPool(string poolID)
        {
            var response = new EmailResponseDto<List<EmailTemplateTable>>();
            try
            {
                var emailTemplates = (from t in _context.EmailTemplates
                                     join p in _context.EmailPools on t.EmailPoolID equals p.ID
                                     where p.ID.ToLower().Equals(poolID.ToLower())
                                     select t).ToList();
                if (emailTemplates.Count > 0)
                {
                    response.Status = true;
                    response.Message = $"Obtained {emailTemplates.Count} records";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Found no record";
                }
                response.Result = emailTemplates;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public EmailResponseDto<string> DeleteEmailTemplate(string templateID)
        {
            var response = new EmailResponseDto<string>();
            try
            {
                var emailTemplate = _context.EmailTemplates.Where(o => o.ID.ToLower().Equals(templateID.ToLower())).FirstOrDefault();
                if (emailTemplate != null)
                {
                    _context.EmailTemplates.Remove(emailTemplate);
                    if (_context.SaveChanges() == 1)
                    {
                        response.Status = true;
                        response.Message = $"Deleted Template";
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = $"Failed to delete Template";
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Found no record";
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
