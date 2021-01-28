using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Nvg.EmailService.Data;

namespace Nvg.EmailService
{
    public class EmailCounterInteractor : IEmailCounterInteractor
    {
        private readonly IEmailCounterRepository _emailCountRepository;

        public EmailCounterInteractor(IEmailCounterRepository emailCountRepository)
        {
            _emailCountRepository = emailCountRepository;
        }

        public string GetEmailCounter(string tenantID, string facilityID)
        {
            return _emailCountRepository.GetEmailCount(tenantID, facilityID);
        }

        public void UpdateEmailCounter(string tenantID, string facilityID)
        {
            _emailCountRepository.UpdateEmailCounter(tenantID, facilityID);
        }
    }
}
