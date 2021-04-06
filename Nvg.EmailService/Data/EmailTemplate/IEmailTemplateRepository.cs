﻿using Nvg.EmailService.Data.Entities;
using Nvg.EmailService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nvg.EmailService.Data.EmailTemplate
{
    public interface IEmailTemplateRepository
    {
        /// <summary>
        /// Adds/Updates the email template in the database.
        /// </summary>
        /// <param name="templateInput"><see cref="EmailTemplateTable"/> model</param>
        /// <returns><see cref="EmailResponseDto{EmailTemplateTable}"/></returns>
        EmailResponseDto<EmailTemplateTable> AddEmailTemplate(EmailTemplateTable templateInput);
        EmailResponseDto<EmailTemplateTable> UpdateEmailTemplate(EmailTemplateTable templateInput);

        /// <summary>
        /// Gets the email template by template Id.
        /// </summary>
        /// <param name="templateID">Template Id</param>
        /// <returns><see cref="EmailTemplateTable"/></returns>
        EmailTemplateTable GetEmailTemplate(string templateID);

        /// <summary>
        /// Gets the email template by template name, channel key and variant.
        /// </summary>
        /// <param name="templateName">Template name</param>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="variant">Variant</param>
        /// <returns><see cref="EmailTemplateTable"/></returns>
        EmailTemplateTable GetEmailTemplate(string templateName, string channelKey, string variant = null);

        /// <summary>
        /// Checks if the template already exists.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="templateName">Template Name</param>
        /// <returns><see cref="EmailResponseDto{bool}"/></returns>
        EmailResponseDto<bool> CheckIfTemplateExist(string channelKey, string templateName);

    }
}
