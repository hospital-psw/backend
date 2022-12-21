﻿namespace HospitalAPI.Controllers.TenderStatistics
{
    using System.Net.Http;
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Routing.Constraints;
    using System.Text.Json;

    public class Connections:IConnections
    {
        public List<double> SendHttpRequestToIntegration(int year)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"http://localhost:45488/api/Tender/money/{year}");
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<List<double>>(json);
            }
        }
    }
}
