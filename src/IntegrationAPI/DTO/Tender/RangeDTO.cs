namespace IntegrationAPI.DTO.Tender
{
    using IntegrationLibrary.Tender;
    using IntegrationLibrary.Tender.Enums;
    using System;
    using System.Collections.Generic;

    public class RangeDTO
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }
}
