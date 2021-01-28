using Nvg.EmailService.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService
{
    public interface IEmailInteractor
    {
        void SendMail(EmailDto emailInputs);
    }
}
