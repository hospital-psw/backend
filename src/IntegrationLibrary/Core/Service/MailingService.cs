namespace IntegrationLibrary.Core.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Mailjet.Client;
    using Mailjet.Client.Resources;
    using Newtonsoft.Json.Linq;
    using Mailjet.Client.TransactionalEmails;

    public class MailingService
    {
        private const string _public_mailjet_key = "6cdf2011e792898a6181e4e0d8c93b0d";
        private const string _private_mailjet_key = "188cd0c4f83511584789aaa4804d6d35";

        private static string generateRandomPassword(int pw_length = 15)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();

            return new string(
                Enumerable.Repeat(chars, pw_length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray()
                );
        }

        static async Task RunAsync(string destination_email, string api_key)
        {
            MailjetClient client = new MailjetClient(_public_mailjet_key, _private_mailjet_key);

            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource
            };

            string email_subject = "Hospital Registration Confirmation";
            string email_body = "<p>Your institution has been successfully registered in our database.</p>" +
                "<p><b>Your API Key is: <b>" + api_key + "</p>" +
                "<p>To confirm the registration go to the following link <a>dummy_link</a>" +
                "and use <b>this email</b> as a username" +
                "and this <b>password: </b>" + generateRandomPassword() + "\n" +
                "for the first login.</p>" +
                "<p>After logging in change your password to a new one.</p>";

            var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact("psw.hospital.2022@gmail.com"))
                .WithSubject(email_subject)
                .WithHtmlPart(email_body)
                .WithTo(new SendContact(destination_email))
                .Build();

            var _ = await client.SendTransactionalEmailAsync(email);
        }

        public static void SendEmail(string destination_mail, string api_key)
        {
            RunAsync(destination_mail, api_key).Wait();
        }
    }
}
