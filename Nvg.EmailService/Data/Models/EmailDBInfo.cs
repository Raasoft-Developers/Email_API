using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailService.Data
{
    public class EmailDBInfo
    {
        public EmailDBInfo(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }
    }
}
