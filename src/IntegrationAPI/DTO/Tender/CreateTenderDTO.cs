namespace IntegrationAPI.DTO.Tender
{
    using IntegrationLibrary.Tender;
    using IntegrationLibrary.Tender.Enums;
    using System;
    using System.Collections.Generic;

    public class CreateTenderDTO
    {
        public DateTime DueDate { get; set; }
        public List<TenderItem> Items { get; set; }
    }
}
