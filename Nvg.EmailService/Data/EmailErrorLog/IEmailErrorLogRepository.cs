using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;
using System.Collections.Generic;

namespace Nvg.EmailService.Data.EmailErrorLog
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