namespace IntegrationAPI.DTO.Tender
{
    using IntegrationLibrary.Tender;
    using System.Collections.Generic;

    public class MakeTenderOfferDTO
    {
        public List<TenderItem> Items { get; set; }
    }
}
