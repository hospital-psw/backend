 namespace HospitalLibrary.Core.Emailing
{
    using MimeKit;
    using MailKit.Net.Smtp;


    public class LoyaltyMailService :ILoyalityMailService
    {

        public LoyaltyMailService()
        {

        }

        public MimeMessage SendEmail(string email)
        {
            var mail = new MimeMessage();

            mail.From.Add(new MailboxAddress("Hospital PSW Team", "ikiakus@gmail.com"));
            mail.To.Add(new MailboxAddress(email, email));
     
            mail.Subject = "Loyality Program";
            mail.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "<b>Thanks for subscribing to our loyality programme! </b>"
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, false);

                smtp.Authenticate("ikiakus@gmail.com", "owql csvn yibq gkex");

                smtp.Send(mail);
                smtp.Disconnect(true);

            }

            return mail;
        }
    }
}
