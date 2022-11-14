using EmailService.Data.Entities;
using EmailService.DTOS;

namespace EmailService.EmailErrorLog
{
    public interface IEmailErrorLogInteractor
    {
        /// <summary>
        /// Adds the email errror log to the database.
        /// </summary>
        /// <param name="errorLogInput"><see cref="EmailErrorLogTable"/></param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<EmailErrorLogTable> AddEmailErrorLog(EmailErrorLogTable errorLogInput);
    }
}