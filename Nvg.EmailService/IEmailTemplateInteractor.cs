using Nvg.EmailService.Dtos;

namespace Nvg.EmailService
{
    public interface IEmailTemplateInteractor
    {
        EmailTemplateDto GetEmailTemplate(long id);
        EmailTemplateDto GetEmailTemplate(string templateName, string tenantID = null, string facilityID = null);
    }
}