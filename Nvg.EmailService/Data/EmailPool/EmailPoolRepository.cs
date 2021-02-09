using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nvg.EmailService.Data.EmailPool
{
    public class EmailPoolRepository : IEmailPoolRepository
    {
        private readonly EmailDbContext _context;

        public EmailPoolRepository(EmailDbContext context)
        {
            _context = context;
        }

        public EmailResponseDto<EmailPoolTable> AddEmailPool(EmailPoolTable emailPoolInput)
        {
            var response = new EmailResponseDto<EmailPoolTable>();
            try
            {
                var isPoolExist = _context.EmailPools.Any(sp => sp.Name.ToLower().Equals(emailPoolInput.Name.ToLower()));
                if (!isPoolExist)
                {
                    emailPoolInput.ID = Guid.NewGuid().ToString();
                    _context.EmailPools.Add(emailPoolInput);
                    if (_context.SaveChanges() == 1)
                    {
                        response.Status = true;
                        response.Message = "Added";
                        response.Result = emailPoolInput;
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Not Added";
                        response.Result = emailPoolInput;
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = "Email pool already exists";
                    response.Result = emailPoolInput;
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

        public EmailResponseDto<EmailPoolTable> GetEmailPoolByName(string poolName)
        {
            var response = new EmailResponseDto<EmailPoolTable>();
            try
            {
                var smsPool = _context.EmailPools.FirstOrDefault(sp => sp.Name.ToLower().Equals(poolName.ToLower()));
                if (smsPool != null)
                {
                    response.Status = true;
                    response.Message = $"Retrieved Email pool data for {poolName}";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Email pool data for {poolName} is not available";
                }
                response.Result = smsPool;
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
