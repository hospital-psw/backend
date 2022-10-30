namespace IntegrationAPI.Controllers
{
    using IntegrationLibrary.BloodBank;
    using IntegrationLibrary.BloodBank.Interfaces;
    using IntegrationLibrary.Util;
    using IntegrationLibrary.Util.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Security.Cryptography;
    using System.Text;

    [ApiController]
    [Route("api/[controller]")]
    public class RegisterBloodBankController
    {
        private readonly IBloodBankService _bloodBankService;
        private readonly IMailSender _mailer;

        public RegisterBloodBankController(IBloodBankService bloodBankService, IMailSender mailSender)
        {
            _bloodBankService = bloodBankService;
            _mailer = mailSender;
        }

        private string ByteArrToString(byte[] byteArr)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < byteArr.Length; i++)
            {
                sb.Append(byteArr[i].ToString("X2"));
            }

            return sb.ToString();
        }

        private string GenerateAPIKey(string mail)
        {
            string current_date = DateTime.Now.ToString();
            string hash_source = mail + current_date;

            byte[] byteSource = ASCIIEncoding.ASCII.GetBytes(hash_source);

            var md5 = new HMACMD5();
            byte[] byteHash = md5.ComputeHash(byteSource);
            string apiKey = ByteArrToString(byteHash);

            return apiKey;
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
