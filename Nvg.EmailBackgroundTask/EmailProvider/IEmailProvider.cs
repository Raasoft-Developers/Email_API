using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nvg.EmailBackgroundTask.EmailProvider
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
    }
}
