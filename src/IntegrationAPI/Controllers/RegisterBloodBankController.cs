namespace IntegrationAPI.Controllers
{
    using IntegrationLibrary.BloodBank;
    using IntegrationLibrary.Util;
    using IntegrationLibrary.Util.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class RegisterBloodBankController
    {
        private readonly BloodBankService _bloodBankService;
        private readonly MailSender _mailer;

        public RegisterBloodBankController(IBloodBankService bloodBankService, IMailSender mailSender)
        {
            _bloodBankService = (BloodBankService)bloodBankService;
            _mailer = (MailSender)mailSender;
        }

        [HttpPost]
        public string RegisterBloodBank(BloodBank bloodBank)
        {
            string apiKey = SecretGenerator.GenerateAPIKey(bloodBank.Email);
            bloodBank.ApiKey = apiKey;

            _bloodBankService.Create(bloodBank);

            var template = MailSender.MakeRegisterTemplate(bloodBank.Email, bloodBank.ApiKey);
            _mailer.SendEmail(template, "Successfull Registration", bloodBank.Email);

            return "Generated key: " + apiKey;
        }
    }
}
