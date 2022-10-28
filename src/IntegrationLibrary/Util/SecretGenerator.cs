namespace IntegrationLibrary.Util
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
    }
}
