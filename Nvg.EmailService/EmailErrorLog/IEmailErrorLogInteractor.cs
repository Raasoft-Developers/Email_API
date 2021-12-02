using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;
using System.Collections.Generic;

namespace Nvg.EmailService.EmailErrorLog
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