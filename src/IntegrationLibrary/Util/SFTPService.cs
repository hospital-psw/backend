namespace IntegrationLibrary.Util
{
    using IntegrationLibrary.Util.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Renci.SshNet;
    using System;
    using System.IO;

    public class SFTPService : ISFTPService
    {
        private readonly ILogger<SFTPService> _logger;
        private readonly IConfiguration _configuration;
        public SFTPService(ILogger<SFTPService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public void SendFile(Stream fileStream)
        {
            string host = _configuration["Sftp:Host"];
            string username = _configuration["Sftp:Username"];
            string password = _configuration["Sftp:Password"];
            int port = int.Parse(_configuration["Sftp:Port"]);

            string remoteDirectory = "/reports/";

            using (SftpClient sftp = new SftpClient(host, port, username, password))
            {
                try
                {
                    sftp.Connect();

                    sftp.UploadFile(fileStream, remoteDirectory + DateTime.Now.Ticks + ".pdf");

                    sftp.Disconnect();
                }
                catch (Exception e)
                {
                    _logger.LogError($"Error in SFTPService in SendFile {e.Message} in {e.StackTrace}");
                }
            }
        }
    }
}
