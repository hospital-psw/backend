namespace HospitalLibrary.Core.Emailing
{
    using MailKit.Net.Smtp;
    using MimeKit;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PenaltyMailService
    {
        public MimeMessage SendEmail(string email)
        {
            var mail = new MimeMessage();

            mail.From.Add(new MailboxAddress("Hospital PSW Team", "ikiakus@gmail.com"));
            mail.To.Add(new MailboxAddress(email, email));

            mail.Subject = "New Working Hours";
            mail.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "<b>We would lik to inform you that your working hours have chanhged </b>" +
                       "<b> log in to your profile to find out more! </b>"
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
