namespace IntegrationAPI.Controllers
{
    using IntegrationLibrary.BloodBank;
    using IntegrationLibrary.Util;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Security.Cryptography;
    using System.Text;

    [ApiController]
    [Route("api/[controller]")]
    public class RegisterBloodBankController
    {
        private readonly BloodBankService _bloodBankService;

        public RegisterBloodBankController(IBloodBankService bloodBankService)
        {
            _bloodBankService = (BloodBankService)bloodBankService;
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
            string api_key = GenerateAPIKey(bloodBank.Email);
            bloodBank.ApiKey = api_key;

            _bloodBankService.Create(bloodBank);
            MailingService.SendEmail(bloodBank.Email, bloodBank.ApiKey);

            return "Generated key: " + api_key;
        }
    }
}
