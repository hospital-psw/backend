namespace IntegrationAPI.DTO.Tender
{
    using IntegrationLibrary.Tender.Enums;
    using IntegrationLibrary.Tender;
    using System;
    using System.Collections.Generic;

    public class GetTenderDTO
    {
        public int Id { get; set; }
        public TenderStatus Status { get; set; }
        public DateTime DueDate { get; set; }
        public List<TenderOffer> Offers { get; set; }
        public TenderOffer TenderWinner { get; set; }
        public List<TenderItem> Items { get; set; }
        public TenderItem TenderItem { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
