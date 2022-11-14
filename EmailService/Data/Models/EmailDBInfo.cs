namespace EmailService.Data
{
    public class EmailDBInfo
    {
        public EmailDBInfo(string connectionString, string databaseProvider)
        {
            ConnectionString = connectionString;
            DatabaseProvider = databaseProvider ?? string.Empty;
        }

        public string ConnectionString { get; set; }
        public string DatabaseProvider { get; set; }
    }
}
