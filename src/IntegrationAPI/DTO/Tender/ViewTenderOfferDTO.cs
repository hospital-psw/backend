namespace IntegrationAPI.DTO.Tender
{
    using IntegrationLibrary.Tender;
    using System;
    using System.Collections.Generic;

    public class ViewTenderOfferDTO
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public List<TenderItem> Items { get; set; }
    }
}
