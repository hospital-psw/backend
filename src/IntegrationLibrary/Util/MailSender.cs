﻿namespace IntegrationLibrary.Util
{
    using grpcServices;
    using IntegrationLibrary.BloodBank;
    using IntegrationLibrary.Tender;
    using IntegrationLibrary.Util.Interfaces;
    using Mailjet.Client;
    using Mailjet.Client.Resources;
    using Mailjet.Client.TransactionalEmails;
    using Microsoft.Extensions.Configuration;
    using Mjml.AspNetCore;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class MailSender : IMailSender
    {
        private readonly IConfiguration _config;
        private readonly IMjmlServices _mjmlServices;

        public MailSender(IConfiguration config, IMjmlServices mjmlServices)
        {
            _config = config;
            _mjmlServices = mjmlServices;
        }

        public static string MakeRegisterTemplate(string mail, string apiKey, string password)
        {
            string basePath = Directory.GetParent(Environment.CurrentDirectory).FullName;
            basePath = Path.Combine(new string[] { basePath, "IntegrationLibrary", "Util", "Email-Templates" });

            string template = "<mjml>" +
                "<mj-body>" +
                "<mj-include path=\"" + Path.Combine(new string[] { basePath, "header.mjml" }) + "\" />" +
                "<mj-include path=\"" + Path.Combine(new string[] { basePath, "welcome_mail_content.mjml" }) + "\" />" +
                "<mj-section background-color=\"#ffffff\" padding-top=\"0\">" +
                "<mj-column width=\"500px\">" +
                "<mj-text font-size=\"16px\" align=\"left\">" +
                "<p>The username is " + mail + "</p>" +
                "<p>The password is " + password + "</p><br/>" +
                "<p>For further communication between our servers use the following API key: <b>" + apiKey + "</b></p>" +
                "</mj-text>" +
                "<mj-include path=\"" + Path.Combine(new string[] { basePath, "attach_to_queue.mjml" }) + "\" />" +
                "<mj-include path=\"" + Path.Combine(new string[] { basePath, "news_sending.mjml" }) + "\" />" +
                "</mj-column>" +
                "</mj-section>" +
                "<mj-include path=\"" + Path.Combine(new string[] { basePath, "footer.mjml" }) + "\" />" +
                "</mj-body>" +
                "</mjml>";

            return template;
        }

        public static string MakeWinningTemplate(int tendId)
        {
            string basePath = Directory.GetParent(Environment.CurrentDirectory).FullName;
            basePath = Path.Combine(new string[] { basePath, "IntegrationLibrary", "Util", "Email-Templates" });

            string template = "<mjml>" +
                "<mj-body>" +
                "<mj-include path=\"" + Path.Combine(new string[] { basePath, "header.mjml" }) + "\" />" +
                "<mj-section background-color=\"#ffffff\" padding-top=\"0\">" +
                "<mj-column width=\"500px\">" +
                "<mj-text font-size=\"16px\" align=\"left\">" +
                "<p>Congratulations, you have received a tender with a number <b>" + tendId + "</b></p>" +
                "</mj-text>" +
                "</mj-column>" +
                "</mj-section>" +
                "<mj-include path=\"" + Path.Combine(new string[] { basePath, "footer.mjml" }) + "\" />" +
                "</mj-body>" +
                "</mjml>";

            return template;
        }

        public static string MakeLoseTemplate(int tendId)
        {
            string basePath = Directory.GetParent(Environment.CurrentDirectory).FullName;
            basePath = Path.Combine(new string[] { basePath, "IntegrationLibrary", "Util", "Email-Templates" });

            string template = "<mjml>" +
                "<mj-body>" +
                "<mj-include path=\"" + Path.Combine(new string[] { basePath, "header.mjml" }) + "\" />" +
                "<mj-section background-color=\"#ffffff\" padding-top=\"0\">" +
                "<mj-column width=\"500px\">" +
                "<mj-text font-size=\"16px\" align=\"left\">" +
                "<p>Tender number <b>" + tendId + "</b> has ended, unfortunately you did not receive it.</p>" +
                "</mj-text>" +
                "</mj-column>" +
                "</mj-section>" +
                "<mj-include path=\"" + Path.Combine(new string[] { basePath, "footer.mjml" }) + "\" />" +
                "</mj-body>" +
                "</mjml>";

            return template;
        }

        public static string MakeUrgentBloodRequestTemplate()
        {
            string basePath = Directory.GetParent(Environment.CurrentDirectory).FullName;
            basePath = Path.Combine(new string[] { basePath, "IntegrationLibrary", "Util", "Email-Templates" });

            string template = "<mjml>" +
                "<mj-body>" +
                "<mj-include path=\"" + Path.Combine(new string[] { basePath, "header.mjml" }) + "\" />" +
                "<mj-section background-color=\"#ffffff\" padding-top=\"0\">" +
                "<mj-column width=\"500px\">" +
                "<mj-text font-size=\"16px\" align=\"left\">" +
                "<p>Your requested report for urgent blood transfers can be found in attachment below.</p>" +
                "</mj-text>" +
                "</mj-column>" +
                "</mj-section>" +
                "<mj-include path=\"" + Path.Combine(new string[] { basePath, "footer.mjml" }) + "\" />" +
                "</mj-body>" +
                "</mjml>";

            return template;
        }

        public static string MakeAcceptBloodUnitTemplate(uint amount, string sender, BloodType bloodType)
        {
            string basePath = Directory.GetParent(Environment.CurrentDirectory).FullName;
            basePath = Path.Combine(new string[] { basePath, "IntegrationLibrary", "Util", "Email-Templates" });

            string template = "<mjml>" +
                "<mj-body>" +
                "<mj-include path=\"" + Path.Combine(new string[] { basePath, "header.mjml" }) + "\" />" +
                "<mj-section background-color=\"#ffffff\" padding-top=\"0\">" +
                "<mj-column width=\"500px\">" +
                "<mj-text font-size=\"16px\" align=\"left\">" +
                "<p>Congratulations, you have succedfully received <b>" + amount + "</b> units of blood, blood type: <b>" + bloodType + "</b> from <b>" + sender + "</b></p>" +
                "</mj-text>" +
                "</mj-column>" +
                "</mj-section>" +
                "<mj-include path=\"" + Path.Combine(new string[] { basePath, "footer.mjml" }) + "\" />" +
                "</mj-body>" +
                "</mjml>";

            return template;
        }

        public static string MakeDeclineBloodUnitTemplate(uint amount, string sender, BloodType bloodType)
        {
            string basePath = Directory.GetParent(Environment.CurrentDirectory).FullName;
            basePath = Path.Combine(new string[] { basePath, "IntegrationLibrary", "Util", "Email-Templates" });

            string template = "<mjml>" +
                "<mj-body>" +
                "<mj-include path=\"" + Path.Combine(new string[] { basePath, "header.mjml" }) + "\" />" +
                "<mj-section background-color=\"#ffffff\" padding-top=\"0\">" +
                "<mj-column width=\"500px\">" +
                "<mj-text font-size=\"16px\" align=\"left\">" +
                "<p>Unfourtunatelly we cannot transfer you <b>" + amount + "</b> units of blood, blood type: <b>" + bloodType + "</b> from <b>" + sender + "</b></p>" +
                "</mj-text>" +
                "</mj-column>" +
                "</mj-section>" +
                "<mj-include path=\"" + Path.Combine(new string[] { basePath, "footer.mjml" }) + "\" />" +
                "</mj-body>" +
                "</mjml>";

            return template;
        }

        public async Task RunAsync(string template, string subject, string destinationEmail, Stream attachment)
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

            TransactionalEmail email;
            if (attachment == null)
            {
                email = new TransactionalEmailBuilder()
                    .WithFrom(new SendContact("psw.hospital.2022@gmail.com"))
                    .WithSubject(subject)
                    .WithHtmlPart(html.Html)
                    .WithTo(new SendContact(destinationEmail))
                    .Build();
            }
            else
            {
                string base64File;
                using (var stream = new MemoryStream())
                {
                    attachment.Position = 0;
                    attachment.CopyTo(stream);
                    byte[] buffer = stream.ToArray();
                    base64File = Convert.ToBase64String(buffer);
                }
                email = new TransactionalEmailBuilder()
                    .WithFrom(new SendContact("psw.hospital.2022@gmail.com"))
                    .WithSubject(subject)
                    .WithHtmlPart(html.Html)
                    .WithTo(new SendContact(destinationEmail))
                    .WithAttachment(new Attachment("report.pdf", "application/pdf", base64File))
                    .Build();
            }

            var _ = await client.SendTransactionalEmailAsync(email);
        }

        public void SendEmail(string template, string subject, string destinationEmail)
        {
            RunAsync(template, subject, destinationEmail, null).Wait();
        }

        public void SendEmail(string template, string subject, string destinationEmail, Stream attachment)
        {
            RunAsync(template, subject, destinationEmail, attachment).Wait();
        }
    }
}
