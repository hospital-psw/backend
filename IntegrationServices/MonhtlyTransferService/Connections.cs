namespace IntegrationServices.MonhtlyTransferService
{
    using IntegrationServices.ReportService.Model;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class Connections
    {
        public static string GetData(string url)
        {
            HttpClient client = new HttpClient();
            var endpoint = new Uri(url);
            var result = client.GetAsync(endpoint).Result;
            var json = result.Content.ReadAsStringAsync().Result;
            return json;
        }

        public static void GetMonthlyBlood()
        {
            MonthlyTransfer mt = new MonthlyTransfer();
            var url = "http://localhost:8081/api/monthlyTransfer";
            HttpClient httpClient = new HttpClient();
            var payload = new StringContent(JsonConvert.SerializeObject(mt), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(url, payload).Result;
            Console.WriteLine(result);



        }
    }
}
