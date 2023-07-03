namespace HospitalLibraryTest.UnitTests
{
    using HospitalLibrary.Core.Emailing;
    using MimeKit;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EmailSendingUnitTest
    {
        private bool isTestEnvironment = true;

        [Fact]
        public void Send_loyalty_confirmation_mail()
        {
            LoyaltyMailService service = new LoyaltyMailService(isTestEnvironment);
            string email = "user@gmail.com";

            MimeMessage result = service.SendEmail(email);

            Assert.NotNull(result);
            Assert.Equal(result.To.First(), new MailboxAddress("ilija.galin00@gmail.com", "ilija.galin00@gmail.com"));
        }

    }
}
