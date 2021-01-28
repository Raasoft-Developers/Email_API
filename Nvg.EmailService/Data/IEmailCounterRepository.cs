using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.Data
{
    public interface IEmailCounterRepository
    {
        string GetEmailCount(string tenantID, string facilityID);
        void UpdateEmailCounter(string tenantID, string facilityID);
    }
}
