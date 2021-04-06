using Nvg.EmailService.DTOS;

namespace Nvg.EmailService.EmailTemplate
{
    public interface IEmailTemplateInteractor
    {
        /// <summary>
        /// Adds/Updates the email template in the database.
        /// </summary>
        /// <param name="templateInput"><see cref="EmailTemplateDto"/> model</param>
        /// <returns><see cref="EmailResponseDto{EmailTemplateDto}"/></returns>
        EmailResponseDto<EmailTemplateDto> AddUpdateEmailTemplate(EmailTemplateDto templateInput);

        EmailTemplateDto GetEmailTemplate(string TemplateID);
        EmailTemplateDto GetEmailTemplate(string templateName, string channelKey, string variant = null);

        /// <summary>
        /// Checks of the template already exists in the database.
        /// </summary>
        /// <param name="channelKey">Channel Key</param>
        /// <param name="templateName">Template Name</param>
        /// <returns><see cref="EmailResponseDto{bool}"/></returns>
        EmailResponseDto<bool> CheckIfTemplateExist(string channelKey, string templateName);
    }
}