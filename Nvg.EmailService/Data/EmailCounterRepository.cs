using Nvg.EmailService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nvg.EmailService.Data
{
    public class EmailCounterRepository : IEmailCounterRepository
    {
        private readonly EmailDbContext _context;

        public EmailCounterRepository(EmailDbContext context)
        {
            _context = context;
        }

        public string GetEmailCount(string tenantID, string facilityID)
        {
            var emailCounterObj = _context.EmailCounter.FirstOrDefault(sc => sc.TenantID == tenantID || sc.FacilityID == facilityID);
            if (emailCounterObj != null)
                return emailCounterObj.Count;
            return null;
        }

        public void UpdateEmailCounter(string tenantID, string facilityID)
        {
            var emailCounterObj = _context.EmailCounter.FirstOrDefault(sc => sc.TenantID == tenantID && sc.FacilityID == facilityID);
            if (emailCounterObj != null)
            {
                var countInt = Convert.ToInt32(emailCounterObj.Count); // TODO Implement encryption 
                countInt += 1;
                emailCounterObj.Count = countInt.ToString();
                _context.EmailCounter.Update(emailCounterObj);
            }
            else
            {
                emailCounterObj = new EmailCounterModel()
                {
                    TenantID = tenantID,
                    FacilityID = facilityID,
                    Count = "1"
                };
                _context.EmailCounter.Add(emailCounterObj);
            }
            _context.SaveChanges();
        }
    }
}
