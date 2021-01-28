using Nvg.EmailService.Dtos;

namespace Nvg.EmailService
{
    public interface IEmailHistoryInteractor
    {
        EmailHistoryDto Add(EmailHistoryDto email);
    }
}