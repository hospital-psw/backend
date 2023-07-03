namespace HospitalLibrary.Core.Emailing
{
    using MimeKit;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ILoyalityMailService
    {
        MimeMessage SendEmail(string email);
    }
}
