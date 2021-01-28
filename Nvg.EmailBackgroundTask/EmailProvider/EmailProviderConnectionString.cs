using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.EmailBackgroundTask.EmailProvider
{
    public class EmailProviderConnectionString
    {
        public string Provider { get; set; } = string.Empty;
        public string ApiUrl { get; set; } = string.Empty;
        public string Sender { get; set; } = string.Empty;
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
            var connString = connectionString.Split(";");
            foreach (var part in connString)
            {
                if (part != "")
                {
                    var splitString = part.Split("=");
                    var key = splitString[0];
                    var value = splitString[1];

                    switch (key)
                    {
                        case "Provider":
                            Provider = value;
                            break;
                        case "ApiUrl":
                            ApiUrl = value;
                            break;
                        case "Sender":
                            Sender = value;
                            break;
                        default:
                            Fields[key] = value;
                            break;
                    }
                }
            }
        }
    }
}