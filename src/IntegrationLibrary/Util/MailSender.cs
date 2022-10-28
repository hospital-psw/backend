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
    using System.Reflection;

    public class MailSender: IMailSender
    {
        private readonly IConfiguration _config;
        private readonly IMjmlServices _mjmlServices;

        public MailSender(IConfiguration config, IMjmlServices mjmlServices)
        {
            _config = config;
            _mjmlServices = mjmlServices;
        }

        public static string MakeRegisterTemplate(string mail, string apiKey)
        {
            string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string template = "{% extends \'" + basePath + "\' %}" +
                "{% block content %}" +
                "<mj-section background-color=\"#ffffff\" padding-top=\"0\">" +
                "<mj-column width=\"500px\">" +
                "<mj-text font-size=\"16px\" align=\"left\">" +
                "<p>The username is " + mail + "</p>" +
                "<p>The password is " + SecretGenerator.generateRandomPassword() + "</p><br/>" +
                "<p>For further communication between our servers use the following API key: <b>" + apiKey + "</b></p>" +
                "{% endblock %}"; 

            return template;
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
