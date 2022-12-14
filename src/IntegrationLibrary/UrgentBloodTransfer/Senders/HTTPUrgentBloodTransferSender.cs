namespace IntegrationLibrary.UrgentBloodTransfer.Senders
{
    using IntegrationLibrary.UrgentBloodTransfer.Interfaces;
    using System.Text.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using IntegrationLibrary.UrgentBloodTransfer.Model;

    public class HTTPUrgentBloodTransferSender : IUrgentBloodTransferSender
    {
        public bool SendUrgentBloodRequest(UrgentBloodTransfer urgentBloodRequest)
        {
            HTTPUrgentBloodTransferRequest urgentBloodTransferRequest = new HTTPUrgentBloodTransferRequest { BloodType = (BloodType)urgentBloodRequest.BloodType, Amount = urgentBloodRequest.Amount };
            HTTPUrgentBloodTransferResponse response;

            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"http://localhost:8081/UrgentBloodRequest");

                var json = JsonSerializer.Serialize(urgentBloodTransferRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var res = client.PostAsync(endpoint, content).Result;
                var resString = res.Content.ReadAsStringAsync().Result;
                response = JsonSerializer.Deserialize<HTTPUrgentBloodTransferResponse>(resString);
            }

            return response.HasBlood;
        }
    }
}
