namespace IntegrationLibrary.Util
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    public class SecretGenerator
    {
        public static string generateRandomPassword(int pwLength = 15)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();

            return new string(
                Enumerable.Repeat(chars, pwLength)
                .Select(s => s[random.Next(s.Length)])
                .ToArray()
                );
        }

        private static string ByteArrToString(byte[] byteArr)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < byteArr.Length; i++)
            {
                sb.Append(byteArr[i].ToString("X2"));
            }

            return sb.ToString();
        }

        public static string GenerateAPIKey(string mail)
        {
            string currentDate = DateTime.Now.ToString();
            string hashSource = mail + currentDate;

            byte[] byteSource = ASCIIEncoding.ASCII.GetBytes(hashSource);

            var md5 = new HMACMD5();
            byte[] byteHash = md5.ComputeHash(byteSource);
            string apiKey = ByteArrToString(byteHash);

            return apiKey;
        }
    }
}
