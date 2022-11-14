using EmailService.Data.Entities;
using EmailService.DTOS;
using System;

namespace EmailService.Data.EmailErrorLog
{
    public class EmailErrorLogRepository : IEmailErrorLogRepository
    {
        private readonly EmailDbContext _context;

        public EmailErrorLogRepository(EmailDbContext context)
        {
            _context = context;
        }

        public EmailResponseDto<EmailErrorLogTable> AddEmailErrorLog(EmailErrorLogTable errorLogInput)
        {
            var response = new EmailResponseDto<EmailErrorLogTable>();
            try
            {
                //    errorLogInput.ID = Guid.NewGuid().ToString();
                errorLogInput = _context.EmailErrorLogs.Add(errorLogInput).Entity;
                if (_context.SaveChanges() == 1)
                {
                    response.Status = true;
                    response.Message = "Email error log has been Added.";
                    response.Result = errorLogInput;
                }
                else
                {
                    response.Status = false;
                    response.Message = "Email error log has not been Added";
                    response.Result = errorLogInput;
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
