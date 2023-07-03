namespace HospitalLibraryTest.Mocks.Mailing
{
    using HospitalLibrary.Core.Emailing;
    using MimeKit;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FakeLoyaltyMailService : ILoyalityMailService
    {

        public LoyaltyMailService _loyaltyMailService;

        public FakeLoyaltyMailService(LoyaltyMailService loyaltyMailService)
        {
            _loyaltyMailService = loyaltyMailService;
        }

        public MimeMessage SendEmail(string email)
        {
            return _loyaltyMailService.SendEmail("ilija.galin00@gmail.com");
        }
    }
}
