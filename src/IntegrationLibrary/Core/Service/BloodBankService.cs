namespace IntegrationLibrary.Core.Service
{
    using IntegrationLibrary.Core.Model;
    using IntegrationLibrary.Core.Repository;
    using IntegrationLibrary.Core.Service.Core;
    using IntegrationLibrary.Settings;
    using System;
    using System.Net.Http;

    public class BloodBankService : BaseService<BloodBank>, IBloodBankService
    {
        public BloodBankService() : base() { }

        public bool CheckBloodType(int id, string type)
        {
            using UnitOfWork unitOfWork = new(new IntegrationDbContext());
            BloodBank bloodBank = unitOfWork.BloodBankRepository.Get(id);

            return sendHttpRequestToBank(bloodBank, type);
        }

        private bool sendHttpRequestToBank(BloodBank bloodBank, string type)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"http://{ bloodBank.ApiUrl }/{bloodBank.GetBloodTypeAvailability}/+{type}");
                client.DefaultRequestHeaders.Add("X-API-KEY", bloodBank.ApiKey);
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                return Convert.ToBoolean(json);
            }
        }
    }
}
