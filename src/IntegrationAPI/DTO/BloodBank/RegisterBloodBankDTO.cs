namespace IntegrationAPI.DTO.BloodBank
{
    public class RegisterBloodBankDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ApiUrl { get; set; }
        public string GetBloodTypeAvailability { get; set; }
        public string GetBloodTypeAndAmountAvailability { get; set; }
    }
}
