using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService
{
    public interface IEmailCounterInteractor
    {
        string GetEmailCounter(string tenantID, string facilityID);
        void UpdateEmailCounter(string tenantID, string facilityID);
    }
}
