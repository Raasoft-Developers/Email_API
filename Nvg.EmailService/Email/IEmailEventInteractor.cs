using Nvg.EmailService.DTOS;

namespace Nvg.EmailService.Email
{
    public interface IEmailEventInteractor
    {
        /// <summary>
        /// Method that will publish the mail to RabbitMQ.
        /// </summary>
        /// <param name="emailInputs"><see cref="EmailDto"/> model</param>
        void SendMail(EmailDto emailInputs);

        /// <summary>
        /// Method that will publish the mail with attachments to RabbitMQ.
        /// </summary>
        /// <param name="emailInputs"><see cref="EmailDto"/> model</param>
        void SendMailWithAttachment(EmailDto emailInputs);

    }
}
