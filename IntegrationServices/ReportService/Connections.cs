namespace IntegrationServices.ReportService
{
    using IntegrationServices.ReportService.DTO;
    using System;
    using System.Collections.Generic;
    using System.IO;
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

        public static string PostData(string url, string json)
        {
            HttpClient client = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var endpoint = new Uri(url);
            var result = client.PostAsync(endpoint, content).Result;
            var retVal = result.Content.ReadAsStringAsync().Result;
            return retVal;
        }

        public static void SendPDFToBB(string filename)
        {
            var url = "http://localhost:8081/api/sendPdf";
            HttpClient httpClient = new HttpClient();

            var fileRoute = @"./../PDF/" + filename;
            var fileName = Path.GetFileName(fileRoute);

            var requestContent = new MultipartFormDataContent();
            var fileStrem = File.OpenRead(fileRoute);
            requestContent.Add(new StreamContent(fileStrem), "pdf", fileName);
            httpClient.PostAsync(url, requestContent).Wait();
        }
    }
}
