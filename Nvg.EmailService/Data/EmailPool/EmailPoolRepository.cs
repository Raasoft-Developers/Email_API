using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;

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
                var emailPool = _context.EmailPools.FirstOrDefault(sp => sp.Name.ToLower().Equals(poolName.ToLower()));
                if (emailPool != null)
                {
                    response.Status = true;
                    response.Message = $"Retrieved Email pool data for {poolName}";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Email pool data for {poolName} is not available";
                }
                response.Result = emailPool;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public EmailResponseDto<List<EmailPoolTable>> GetEmailPools()
        {
            var response = new EmailResponseDto<List<EmailPoolTable>>();
            try
            {
                var emailPools = _context.EmailPools.ToList();
                if (emailPools.Count > 0)
                {
                    response.Status = true;
                    response.Message = $"Retrieved Email pool data";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Email pool data is not available";
                }
                response.Result = emailPools;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public EmailResponseDto<List<EmailPoolTable>> GetEmailPoolNames()
        {
            var response = new EmailResponseDto<List<EmailPoolTable>>();
            try
            {
                var emailPools = _context.EmailPools.ToList();
                if (emailPools.Count > 0)
                {
                    response.Status = true;
                    response.Message = $"Retrieved Email pool data";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"Email pool data is not available";
                }
                response.Result = emailPools;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public EmailResponseDto<EmailPoolTable> UpdateEmailPool(EmailPoolTable emailPoolInput)
        {
            var response = new EmailResponseDto<EmailPoolTable>();
            try
            {
                var queryResult = _context.EmailPools.Where(sp => sp.ID.ToLower().Equals(emailPoolInput.ID.ToLower())).FirstOrDefault();
                if (queryResult != null)
                {
                    queryResult.Name = emailPoolInput.Name;
                    if (_context.SaveChanges() == 1)
                    {
                        response.Status = true;
                        response.Message = "Updated";
                        response.Result = emailPoolInput;
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Not Updated";
                        response.Result = emailPoolInput;
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = "No Record found.";
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

        public EmailResponseDto<string> DeleteEmailPool(string poolID)
        {
            var response = new EmailResponseDto<string>();
            try
            {
                var queryResult = _context.EmailPools.Where(sp => sp.ID.ToLower().Equals(poolID.ToLower())).FirstOrDefault();
                if (queryResult != null)
                {
                    _context.Remove(queryResult);
                    if (_context.SaveChanges() == 1)
                    {
                        response.Status = true;
                        response.Message = "Deleted";
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Not Deleted";
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = "No Record found.";
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
