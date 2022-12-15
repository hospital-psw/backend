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

        public static void GetMonthlyBlood(MonthlyTransfer mt)
        {
            var url = "http://localhost:8081/api/monthlyTransfer";
            HttpClient httpClient = new HttpClient();
            var payload = new StringContent(JsonConvert.SerializeObject(mt), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(url, payload).Result;
            //Console.WriteLine(result);
        }

        public static void UpdateBloodUnit(string message)
        {
            //FORMAT= A_PLUS:{BROJ}-B_PLUS:{BROJ}...
            var bloodUnits = message.Split('-');
            HttpClient client = new HttpClient();
            var endpoint = new Uri("http://localhost:16177/api/BloodUnit");
            foreach (var bloodUnit in bloodUnits)
            {
                var unit = new BloodUnit((BloodType)Enum.Parse(typeof(BloodType), bloodUnit.Split(':')[0]), Int32.Parse(bloodUnit.Split(':')[1]));
                var payload = new StringContent(JsonConvert.SerializeObject(unit), Encoding.UTF8, "application/json");
                var result = client.PutAsync(endpoint, payload).Result;
            }
        }


    }
}
