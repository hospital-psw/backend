namespace IntegrationAPI.DTO
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SaveConfigurationDTO
    {
        [Required]
        public int Id { get; set; }
        public DateTime ReportTo { get; set; }
        public DateTime ReportFrom { get; set; }
        public int Frequently { get; set; }
    }
}
