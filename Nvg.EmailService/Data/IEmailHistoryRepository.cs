using Nvg.EmailService.Data.Entities;

namespace Nvg.EmailService.Data
{
    public interface IEmailHistoryRepository
    {
        EmailHistoryModel Add(EmailHistoryModel email);
    }
}