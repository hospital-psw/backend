namespace IntegrationLibrary.Util
{
    using Microsoft.Extensions.Configuration;
    using IntegrationLibrary.Util.Interfaces;
    using Mailjet.Client;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Mailjet.Client.Resources;
    using Mailjet.Client.TransactionalEmails;
    using Microsoft.AspNetCore.Mvc;
    using Mjml.AspNetCore;
    using System.IO;

    public class MailSender: IMailSender
    {
        private readonly IConfiguration _config;
        private readonly IMjmlServices _mjmlServices;

        public MailSender(IConfiguration config, IMjmlServices mjmlServices)
        {
            _config = config;
            _mjmlServices = mjmlServices;
        }

        public string LoadTemplate(string templatePath)
        {
            return File.ReadAllText(templatePath);
        }

        public async Task RunAsync(string template, string subject, string destinationEmail)
        {
            MailjetClient client = new MailjetClient(
                _config.GetValue<string>("PublicMailjetKey"),
                _config.GetValue<string>("PrivateMailjetKey")
                );

            MailjetRequest req = new MailjetRequest
            {
                Resource = Send.Resource
            };

            var html = await _mjmlServices.Render(template);

            var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact("psw.hospital.2022@gmail.com"))
                .WithSubject(subject)
                .WithHtmlPart(html.Html)
                .WithTo(new SendContact(destinationEmail))
                .Build();

            var _ = await client.SendTransactionalEmailAsync(email);
        }

        public void SendEmail(string template, string subject, string destinationEmail)
        {
            RunAsync(template, subject, destinationEmail).Wait();
        }
    }
}
