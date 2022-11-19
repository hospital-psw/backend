namespace IntegrationAPI.DTO.BloodBank
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UpdateBloodBankDTO
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string ApiUrl { get; set; }
        public string GetBloodTypeAvailability { get; set; }
        public string GetBloodTypeAndAmountAvailability { get; set; }
        public DateTime ReportTo { get; set; }
        public DateTime ReportFrom { get; set; }
        public int Frequently { get; set; }
    }
}
