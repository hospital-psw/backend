namespace IntegrationServices.ReportService.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BloodBank : Entity
    {
        public BloodBank() { }
        public BloodBank(string name, string email, string apiUrl, string apiKey, string getBloodTypeAvailability, string getBloodTypeAndAmountAvailability, string adminPassword, bool isDummyPassword, DateTime reportTo, DateTime reportFrom, int frequently)
        {
            Name = name;
            Email = email;
            ApiKey = apiKey;
            ApiUrl = apiUrl;
            GetBloodTypeAndAmountAvailability = getBloodTypeAndAmountAvailability;
            GetBloodTypeAvailability = getBloodTypeAvailability;
            AdminPassword = adminPassword;
            IsDummyPassword = isDummyPassword;
            ReportTo = reportTo;
            ReportFrom = reportFrom;
            Frequently = frequently;
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ApiUrl { get; set; }
        public string ApiKey { get; set; }
        public string GetBloodTypeAvailability { get; set; }
        public string GetBloodTypeAndAmountAvailability { get; set; }
        public string AdminPassword { get; set; }
        public bool IsDummyPassword { get; set; }
        public DateTime ReportTo { get; set; }
        public DateTime ReportFrom { get; set; }
        public int Frequently { get; set; }
    }
}
