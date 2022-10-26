namespace IntegrationAPI.Middleware
{
    using IntegrationLibrary.Core.Service;
    using IntegrationLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class APIKeyOptions
    {
        // if true this middleware will be used in all endpoints
        // if false it will only work on provided endpoints
        public List<string> Endpoints { get; set; }
    }

    public class APIKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly APIKeyOptions _options;
        private const string APIKEY_PARAM = "x-api-key";

        public APIKeyMiddleware(RequestDelegate next, APIKeyOptions options)
        {
            _next = next;
            _options = options;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine(context.Request.Path);
            // check if middleware should be used based on endpoint
            if (!_options.Endpoints.Contains(context.Request.Path))
                await _next(context);

            else
            {
                // try to extract key, if there is none, return 401
                // if there is a key, place it into extracedAPIKey
                if (!context.Request.Headers.TryGetValue(APIKEY_PARAM, out var extractedAPIKey))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("API Key not provided");
                    return;
                }

                // get valid keys from injected service
                var _bankService = (BloodBankService)context.RequestServices.GetRequiredService<IBloodBankService>();
                List<string> valid_keys = _bankService.GetAll().Select(bank => bank.ApiKey).ToList();

                // check validity of key
                if (!valid_keys.Contains(extractedAPIKey))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized access");
                    return;
                }

                await _next(context);
            }
        }
    }
}
