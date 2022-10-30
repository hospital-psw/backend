using IntegrationLibrary.Core;

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
        public string IsDummyPassword { get; set; }   
    }
}
