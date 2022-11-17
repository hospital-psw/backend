namespace IntegrationAPI.DTO.BloodBank
{
    using System;

    public class SaveConfigurationDTO
    {
        public int Id { get; set; }
        public DateTime ReportTo { get; set; }
        public DateTime ReportFrom { get; set; }
        public int Frequently { get; set; }
    }
}
