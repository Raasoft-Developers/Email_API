using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nvg.EmailBackgroundTask.EmailProvider
{
    public interface IEmailProvider
    {
        public Task<string> SendEmail(List<string> recipients, string message, string subject, string sender = null);
    }
}
