using Nvg.EmailService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.Data
{
    public class EmailHistoryRepository : IEmailHistoryRepository
    {
        private readonly EmailDbContext _context;

        public EmailHistoryRepository(EmailDbContext context)
        {
            _context = context;
        }

        public EmailHistoryModel Add(EmailHistoryModel email)
        {
            email = _context.EmailHistory.Add(email).Entity;
            _context.SaveChanges();
            return email;
        }

    }
}
