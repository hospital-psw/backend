namespace IntegrationAPI.DTO.UrgentBloodTransfer
{
    using System;

    public class GenerateReportDTO
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool SendEmail { get; set; }
    }
}
