using Nvg.EmailService.DTOS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nvg.EmailService.EmailServiceProviders
{
    public interface IEmailProvider
    {
        /// <summary>
        /// Send the email via the provider.
        /// </summary>
        /// <param name="recipients">List of recipients</param>
        /// <param name="message">Message body</param>
        /// <param name="subject">Subject</param>
        /// <param name="sender">Sender</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public Task<string> SendEmail(List<string> recipients, string message, string subject, string sender = null);


        /// <summary>
        /// Send the email via the provider.
        /// </summary>
        /// <param name="recipients">List of recipients</param>
        /// <param name="files"><see cref="IFormFileCollection"></param>
        /// <param name="message">Message body</param>
        /// <param name="subject">Subject</param>
        /// <param name="sender">Sender</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public Task<string> SendEmailWithAttachments(List<string> recipients, List<EmailAttachment> files, string message, string subject, string sender = null);
    }
}
