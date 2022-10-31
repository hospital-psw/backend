namespace IntegrationLibrary.Util
{
    using IntegrationLibrary.BloodBank;
    using IntegrationLibrary.Util.Interfaces;
    using System;
    using System.Net.Http;

    public class BBConnections : IBBConnections
    {
        public bool SendHttpRequestToBank(BloodBank bloodBank, string type)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"http://{bloodBank.ApiUrl}/{bloodBank.GetBloodTypeAvailability.Replace("!BLOOD_TYPE", type)}");
                client.DefaultRequestHeaders.Add("x-api-key", bloodBank.ApiKey);
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                return Convert.ToBoolean(json);
            }
        }
    }

}
