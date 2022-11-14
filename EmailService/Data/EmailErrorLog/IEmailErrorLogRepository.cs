using EmailService.Data.Entities;
using EmailService.DTOS;

namespace EmailService.Data.EmailErrorLog
{
    public interface IEmailErrorLogRepository
    {
        /// <summary>
        /// Adds the Email Error Log into the database.
        /// </summary>
        /// <param name="errorLogInput"><see cref="EmailErrorLogTable"/> model</param>
        /// <returns><see cref="EmailResponseDto{T}"/> model</returns>
        EmailResponseDto<EmailErrorLogTable> AddEmailErrorLog(EmailErrorLogTable errorLogInput);

    }
}