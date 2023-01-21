using IntegrationLibrary.Core;
using System;

namespace IntegrationLibrary.BloodBank
{
    public class BloodBank : Entity
    {
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
        public MonthlyTransfer MonthlyTransfer { get; set; }
    }
}
