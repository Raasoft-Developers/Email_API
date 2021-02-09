using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.Data.EmailPool
{
    public interface IEmailPoolRepository
    {
        /// <summary>
        /// Adds the email pool into the database.
        /// </summary>
        /// <param name="emailPoolInput"><see cref="EmailPoolTable"/> model</param>
        /// <returns><see cref="EmailResponseDto{EmailPoolTable}"/></returns>
        EmailResponseDto<EmailPoolTable> AddEmailPool(EmailPoolTable emailPoolInput);

        /// <summary>
        /// Gets the email pool by pool name.
        /// </summary>
        /// <param name="poolName">Pool name</param>
        /// <returns><see cref="EmailResponseDto{EmailPoolTable}"/></returns>
        EmailResponseDto<EmailPoolTable> GetEmailPoolByName(string poolName);
    }
}
