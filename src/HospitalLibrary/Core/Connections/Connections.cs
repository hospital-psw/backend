namespace HospitalAPI.Controllers.TenderStatistics
{
    using Microsoft.AspNetCore.Routing.Constraints;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text.Json;

    public class Connections : IConnections
    {
        private readonly IConfiguration _configuration;
        public Connections(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<double> SendHttpRequestToIntegration(int year)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"{_configuration["IntegrationAPI"]}/api/Tender/money/{year}");
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<List<double>>(json);
            }
        }

        public List<double> SendHttpRequestToIntegrationBloodQuantity(int year, int bloodType)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"{_configuration["IntegrationAPI"]}/api/Tender/blood/{year}/{bloodType}");
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<List<double>>(json);
            }
        }
    }
}
