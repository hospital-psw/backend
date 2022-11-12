namespace IntegrationAPI.DTO.BloodBank
{
    public class GetBloodBankDTO
    {
        public int Id { get; set; }
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
