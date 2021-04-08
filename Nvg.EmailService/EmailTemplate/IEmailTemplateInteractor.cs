using Nvg.EmailService.DTOS;

namespace Nvg.EmailService.EmailTemplate
{
    public interface IEmailTemplateInteractor
    {
        /// <summary>
        /// Adds the email template to the database.
        /// </summary>
        /// <param name="templateInput"><see cref="EmailTemplateDto"/> model</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<EmailTemplateDto> AddEmailTemplate(EmailTemplateDto templateInput);

        /// <summary>
        /// Updates the email template in the database.
        /// </summary>
        /// <param name="templateInput"><see cref="EmailTemplateDto"/> model</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<EmailTemplateDto> UpdateEmailTemplate(EmailTemplateDto templateInput);

        /// <summary>
        /// Gets the Email Template by ID.
        /// </summary>
        /// <param name="templateID">Template ID</param>
        /// <returns><see cref="EmailTemplateDto"/></returns>
        EmailTemplateDto GetEmailTemplate(string templateID);

        /// <summary>
        /// Gets the Email Template by name, channelkey and variant.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="templateName">Template Name</param>
        /// <param name="variant">Variant</param>
        /// <returns><see cref="EmailTemplateDto"/></returns>
        EmailTemplateDto GetEmailTemplate(string templateName, string channelKey, string variant = null);

        /// <summary>
        /// Checks of the template already exists in the database.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="templateName">Template Name</param>
        /// <returns><see cref="EmailResponseDto{T}"/></returns>
        EmailResponseDto<bool> CheckIfTemplateExist(string channelKey, string templateName);
    }
}