namespace IntegrationLibrary.Util
{
    using IntegrationLibrary.Tender.Enums;
    using IntegrationLibrary.Util.Interfaces;
    using Mailjet.Client;
    using Mailjet.Client.Resources;
    using Mailjet.Client.TransactionalEmails;
    using Microsoft.Extensions.Configuration;
    using Mjml.AspNetCore;
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using static IdentityServer4.Models.IdentityResources;

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

        public static string MakeBloodTransferTemplate(string bloodType, double amount)
        {
            string basePath = Directory.GetParent(Environment.CurrentDirectory).FullName;
            basePath = Path.Combine(new string[] { basePath, "IntegrationLibrary", "Util", "Email-Templates" });

            string template = "<mjml>" +
                "<mj-body>" +
                "<mj-include path=\"" + Path.Combine(new string[] { basePath, "header.mjml" }) + "\" />" +
                "<mj-section background-color=\"#ffffff\" padding-top=\"0\">" +
                "<mj-column width=\"500px\">" +
                "<mj-text font-size=\"16px\" align=\"left\">" +
                "<p>" + amount + " units of</p>" +
                "<p>blood type " + bloodType + "</p>" +
                //"<p>" + isAvaliable + " in our stocks.</p><br/>" +
                "</mj-text>" +
                "</mj-column>" +
                "</mj-section>" +
                "<mj-include path=\"" + Path.Combine(new string[] { basePath, "footer.mjml" }) + "\" />" +
                "</mj-body>" +
                "</mjml>";

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
