namespace IntegrationLibrary.Util
{
    using grpcServices;
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
            //string basePath = Directory.GetParent(Environment.CurrentDirectory).FullName;
            //basePath = Path.Combine(new string[] { basePath, "IntegrationLibrary", "Util", "Email-Templates" });

            string template = "<mjml>" +
                "<mj-body>" +
                "<mj-include path=\"" + HeaderMjml() + "\" />" +
                "<mj-include path=\"" + WelcomeMailContentMjml() + "\" />" +
                "<mj-section background-color=\"#ffffff\" padding-top=\"0\">" +
                "<mj-column width=\"500px\">" +
                "<mj-text font-size=\"16px\" align=\"left\">" +
                "<p>The username is " + mail + "</p>" +
                "<p>The password is " + password + "</p><br/>" +
                "<p>For further communication between our servers use the following API key: <b>" + apiKey + "</b></p>" +
                "</mj-text>" +
                "<mj-include path=\"" + AttachToQueueMjml() + "\" />" +
                "<mj-include path=\"" + NewsSendingMjml() + "\" />" +
                "</mj-column>" +
                "</mj-section>" +
                "<mj-include path=\"" + FooterMjml() + "\" />" +
                "</mj-body>" +
                "</mjml>";

            return template;
        }

        public static string MakeWinningTemplate(int tendId)
        {
            //string basePath = Directory.GetParent(Environment.CurrentDirectory).FullName;
            //basePath = Path.Combine(new string[] { basePath, "IntegrationLibrary", "Util", "Email-Templates" });

            string template = "<mjml>" +
                "<mj-body>" +
                "<mj-include path=\"" + HeaderMjml() + "\" />" +
                "<mj-section background-color=\"#ffffff\" padding-top=\"0\">" +
                "<mj-column width=\"500px\">" +
                "<mj-text font-size=\"16px\" align=\"left\">" +
                "<p>Congratulations, you have received a tender with a number <b>" + tendId + "</b></p>" +
                "</mj-text>" +
                "</mj-column>" +
                "</mj-section>" +
                "<mj-include path=\"" + FooterMjml() + "\" />" +
                "</mj-body>" +
                "</mjml>";

            return template;
        }

        public static string MakeLoseTemplate(int tendId)
        {
            //string basePath = Directory.GetParent(Environment.CurrentDirectory).FullName;
            //basePath = Path.Combine(new string[] { basePath, "IntegrationLibrary", "Util", "Email-Templates" });

            string template = "<mjml>" +
                "<mj-body>" +
                "<mj-include path=\"" + HeaderMjml() + "\" />" +
                "<mj-section background-color=\"#ffffff\" padding-top=\"0\">" +
                "<mj-column width=\"500px\">" +
                "<mj-text font-size=\"16px\" align=\"left\">" +
                "<p>Tender number <b>" + tendId + "</b> has ended, unfortunately you did not receive it.</p>" +
                "</mj-text>" +
                "</mj-column>" +
                "</mj-section>" +
                "<mj-include path=\"" + FooterMjml() + "\" />" +
                "</mj-body>" +
                "</mjml>";

            return template;
        }

        public static string MakeUrgentBloodRequestTemplate()
        {
            //string basePath = Directory.GetParent(Environment.CurrentDirectory).FullName;
            //basePath = Path.Combine(new string[] { basePath, "IntegrationLibrary", "Util", "Email-Templates" });

            string template = "<mjml>" +
                "<mj-body>" +
                "<mj-include path=\"" + HeaderMjml() + "\" />" +
                "<mj-section background-color=\"#ffffff\" padding-top=\"0\">" +
                "<mj-column width=\"500px\">" +
                "<mj-text font-size=\"16px\" align=\"left\">" +
                "<p>Your requested report for urgent blood transfers can be found in attachment below.</p>" +
                "</mj-text>" +
                "</mj-column>" +
                "</mj-section>" +
                "<mj-include path=\"" + FooterMjml() + "\" />" +
                "</mj-body>" +
                "</mjml>";

            return template;
        }

        public static string MakeAcceptBloodUnitTemplate(uint amount, string sender, BloodType bloodType)
        {
            //string basePath = Directory.GetParent(Environment.CurrentDirectory).FullName;
            //basePath = Path.Combine(new string[] { basePath, "IntegrationLibrary", "Util", "Email-Templates" });

            string template = "<mjml>" +
                "<mj-body>" +
                "<mj-include path=\"" + HeaderMjml() + "\" />" +
                "<mj-section background-color=\"#ffffff\" padding-top=\"0\">" +
                "<mj-column width=\"500px\">" +
                "<mj-text font-size=\"16px\" align=\"left\">" +
                "<p>Congratulations, you have succedfully received <b>" + amount + "</b> units of blood, blood type: <b>" + bloodType + "</b> from <b>" + sender + "</b></p>" +
                "</mj-text>" +
                "</mj-column>" +
                "</mj-section>" +
                "<mj-include path=\"" + FooterMjml() + "\" />" +
                "</mj-body>" +
                "</mjml>";

            return template;
        }

        public static string MakeDeclineBloodUnitTemplate(uint amount, string sender, BloodType bloodType)
        {
            //string basePath = Directory.GetParent(Environment.CurrentDirectory).FullName;
            //basePath = Path.Combine(new string[] { basePath, "IntegrationLibrary", "Util", "Email-Templates" });

            string template = "<mjml>" +
                "<mj-body>" +
                "<mj-include path=\"" + HeaderMjml() + "\" />" +
                "<mj-section background-color=\"#ffffff\" padding-top=\"0\">" +
                "<mj-column width=\"500px\">" +
                "<mj-text font-size=\"16px\" align=\"left\">" +
                "<p>Unfourtunatelly we cannot transfer you <b>" + amount + "</b> units of blood, blood type: <b>" + bloodType + "</b> from <b>" + sender + "</b></p>" +
                "</mj-text>" +
                "</mj-column>" +
                "</mj-section>" +
                "<mj-include path=\"" + FooterMjml() + "\" />" +
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

        private static string HeaderMjml()
        {
            return "<mj-section background-color=\"#9be5aa\" padding-top=\"0\" padding-bottom=\"0\">\r\n    <mj-column>\r\n        <mj-image align=\"center\" width=\"150px\" src=\"https://media.istockphoto.com/vectors/caduceus-medical-symbol-vector-id1215977731?k=20&m=1215977731&s=612x612&w=0&h=EvFkHQ9BNDAiCno7Qm19In-syJPUYe5br61T7GyOTc8=\" />\r\n    </mj-column>\r\n</mj-section>";
        }

        private static string FooterMjml()
        {
            return "<mj-section background-color=\"#9be5aa\" padding-top=\"0\" padding-bottom=\"0\">\r\n    <mj-column>\r\n        <mj-text align=\"center\" font-size=\"12px\" padding=\"0\">\r\n            <p>PSW Hospital 69420 Random Road, Novi Sad, Serbia</p>\r\n            <p>Phone number: 021 69420</p>\r\n        </mj-text>\r\n    </mj-column>\r\n</mj-section>";
        }

        private static string WelcomeMailContentMjml()
        {
            return "<mj-section background-color=\"#ffffff\" padding-top=\"0\">\r\n    <mj-column width=\"500px\">\r\n        <mj-text font-size=\"20px\" align=\"center\">\r\n            <p>Your institution has been registered to our hospital!</p>\r\n        </mj-text>\r\n        <mj-text font-size=\"16px\" align=\"left\">\r\n            <p>To confirm this registration fill out the change password form on the following <a href=\"http://localhost:4200/changePassword\">link</a></p>\r\n        </mj-text>\r\n    </mj-column>\r\n</mj-section>";
        }
        private static string AttachToQueueMjml()
        {
            return "<mj-text font-size=\"16px\" align=\"left\">\r\n    <p>To be able to send Your news to us, You will need to open a connection with our message queue</p>\r\n    <p>To do so, follow the guidelines below</p>\r\n    <ul>\r\n        <li>Download and install RabbitMQ message queue service</li>\r\n        <li>Declare a connection towards <b>Hostname=localhost</b></li>\r\n        <li>Declare a queue with the name <b>hello</b></li>\r\n        <li>All other queue parameters should be set to <b>false</b> or <b>null</b></li>\r\n    </ul>\r\n</mj-text>";
        }
        private static string NewsSendingMjml()
        {
            return "<mj-text font-size=\"16px\" align=\"left\">\r\n    <p>To send news to our establishment follow the guidelines below:</p>\r\n    <ul>\r\n        <li>News image <b>extension</b></li>\r\n        <li><b>Delimiter</b> consisting of a newline character followed by 20 '-' characters</li>\r\n        <li>News <b>image data</b> encoded in Base64</li>\r\n        <li><b>Delimiter</b></li>\r\n        <li>News <b>title</b></li>\r\n        <li><b>Delimiter</b></li>\r\n        <li>News <b>body</b></li>\r\n    </ul>\r\n</mj-text>";
        }
    }
}
