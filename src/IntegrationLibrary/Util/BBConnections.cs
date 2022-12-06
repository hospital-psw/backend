namespace IntegrationLibrary.Util
{
    using IntegrationLibrary.BloodBank;
    using IntegrationLibrary.UrgentBloodTransfer;
    using IntegrationLibrary.Util.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;

    public class BBConnections : IBBConnections
    {
        public async void SendBloodUnitToHospital(BloodUnit unit)
        {
            using (var client = new HttpClient())
            {
                var json = JsonSerializer.Serialize(unit);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var endpoint = new Uri("http://localhost:16177/api/BloodUnit");
                var response = await client.PostAsync(endpoint, content);
                var resString = response.Content.ReadAsStringAsync();
            }
        }

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
