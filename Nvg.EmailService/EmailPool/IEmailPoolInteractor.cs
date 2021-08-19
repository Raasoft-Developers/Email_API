using Nvg.EmailService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.EmailPool
{
    public interface IEmailPoolInteractor
    {
        /// <summary>
        /// Adds the email pool to the table.
        /// </summary>
        /// <param name="emailPoolInput"><see cref="EmailPoolDto"/> model</param>
        /// <returns><<see cref="EmailResponseDto{T}"/>/returns>
        EmailResponseDto<EmailPoolDto> AddEmailPool(EmailPoolDto emailPoolInput);
    }
}
