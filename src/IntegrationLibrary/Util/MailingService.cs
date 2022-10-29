namespace IntegrationLibrary.Util
{
    using Mailjet.Client;
    using Mailjet.Client.Resources;
    using Mailjet.Client.TransactionalEmails;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class MailingService
    {
        private const string _publicMailjetKey = "6cdf2011e792898a6181e4e0d8c93b0d";
        private const string _privateMailjetKey = "188cd0c4f83511584789aaa4804d6d35";

        private static string generateRandomPassword(int pwLength = 15)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();

            return new string(
                Enumerable.Repeat(chars, pwLength)
                .Select(s => s[random.Next(s.Length)])
                .ToArray()
                );
        }

        static async Task RunAsync(string destinationEmail, string apiKey)
        {
            MailjetClient client = new MailjetClient(_publicMailjetKey, _privateMailjetKey);

            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource
            };

            string emailSubject = "Hospital Registration Confirmation";
            string emailBody = "" +
                "<p>Your institution has been successfully registered in our database.</p>" +
                "<p><b>Your API Key is: <b>" + apiKey + "</p>" +
                "<p>To confirm the registration go to the following link <a>dummy_link</a>\n" +
                "and use <b>this email</b> as a username\n" +
                "and this <b>password: </b>" + generateRandomPassword() + "\n" +
                "for the first login.</p>" +
                "<p>After logging in change your password to a new one.</p>";

            var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact("psw.hospital.2022@gmail.com"))
                .WithSubject(emailSubject)
                .WithHtmlPart(emailBody)
                .WithTo(new SendContact(destinationEmail))
                .Build();

            var _ = await client.SendTransactionalEmailAsync(email);
        }

        public static void SendEmail(string destinationEmail, string apiKey)
        {
            RunAsync(destinationEmail, apiKey).Wait();
        }
    }
}
