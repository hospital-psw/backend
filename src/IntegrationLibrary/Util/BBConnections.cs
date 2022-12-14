namespace IntegrationLibrary.Util
{
    using IntegrationLibrary.BloodBank;
    using IntegrationLibrary.UrgentBloodTransfer.Model;
    using IntegrationLibrary.Util.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text;
    using System.Text.Json;

    public class BBConnections : IBBConnections
    {
        public async void SendBloodUnitToHospital(BloodUnit unit)
        {
            using (var client = new HttpClient())
            {

                var getEndpoint = new Uri($"http://localhost:16177/api/BloodUnit/get/{unit.BloodType}");
                var getResponse = await client.GetAsync(getEndpoint);
                BloodUnit hospitalBloodUnit = await getResponse.Content.ReadFromJsonAsync<BloodUnit>();

                hospitalBloodUnit.Amount += unit.Amount;

                var json = JsonSerializer.Serialize(hospitalBloodUnit);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var putEndpoint = new Uri("http://localhost:16177/api/BloodUnit");
                var response = await client.PutAsync(putEndpoint, content);
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
