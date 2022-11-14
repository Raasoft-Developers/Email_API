using System;
using System.Collections.Generic;

namespace EmailService.EmailServiceProviders
{
    public class EmailProviderConnectionString
    {
        public Dictionary<string, string> Fields { get; set; }

        public EmailProviderConnectionString(string connectionString)
        {
            Fields = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(connectionString))
            {
                LoadConnectionStringParts(connectionString);
            }
            else
            {
                throw new ArgumentException(nameof(connectionString));
            }
        }

        private void LoadConnectionStringParts(string connectionString)
        {
            var connString = connectionString.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in connString)
            {
                if (part != "")
                {
                    var splitString = part.Split(new[] { '=' }, 2);
                    var key = splitString[0];
                    var value = splitString[1];
                    Fields[key] = value;
                }
            }
        }
    }
}
