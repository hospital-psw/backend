namespace HospitalAPI.EmailServices
{
    using HospitalAPI.Configuration;
    using HospitalLibrary.Core.Model;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;

    public class EmailService : IEmailService
    {
        private ProjectConfiguration _configuration;
        private ILogger<EmailService> _logger;

        public EmailService(ProjectConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task Send()
        {
            try
            {
                string body = "<p>Dear, Djomla" +
                              "</p><p> We inform you that your appointment set at 20.01.2023." +
                              "has been canceled by doctor Ocokoljic </p>" +
                              "<p>Sorry if we upset your daily plans, you can always contact us, or schedule a new appointment</p>" +
                              "<p>Best Regards, PSW Hospital </p>";
                MailMessage mailMessage = CreateEmailMessage(body, "Appointment cancelation");
                mailMessage.To.Add("milos.gravara1@gmail.com, vuk.milanovic11@gmail.com");

                await SendEmailMessage(mailMessage);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in EmailService in Send {e.Message} in {e.StackTrace}");
            }
        }

        private MailMessage CreateEmailMessage(string body, string subject = "New Message")
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(_configuration.EmailSettings.FromEmail);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                return message;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in HospitalAPI EmailService in CreateEmailMessage {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        private async Task SendEmailMessage(MailMessage message)
        {
            using SmtpClient client = new(_configuration.EmailSettings.ServerAddress, _configuration.EmailSettings.Port)
            {
                Credentials = new NetworkCredential(_configuration.EmailSettings.Username, _configuration.EmailSettings.Password),
                EnableSsl = _configuration.EmailSettings.EnableSsl,
            };

            await client.SendMailAsync(message);
        }
    }
}
