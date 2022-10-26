namespace HospitalAPI.Configuration
{
    public class ProjectConfiguration : IProjectConfiguration
    {
        public DatabaseConfiguration DatabaseConfiguration { get; set; } = new DatabaseConfiguration();

        public EmailSettings EmailSettings { get; set; } = new EmailSettings();
    }

    public class DatabaseConfiguration
    {
        public string ConnectionString { get; set; }
    }

    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
    }

    public class EmailSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string FromEmail { get; set; }
        public string DisplayName { get; set; }
        public string ServerAddress { get; set; }
        public bool EnableSsl { get; set; }
    }
}
