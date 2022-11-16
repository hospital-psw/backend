namespace IntegrationServices.RabbitMQServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Net.Http;

    public class PostMessageToAPI
    {
        private static readonly HttpClient client = new HttpClient();

        public static async void PostMessage(string json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:45488/news", content);

            Console.WriteLine(response.Content.ReadAsStringAsync());
        }
    }
}
