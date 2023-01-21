namespace IntegrationAPI.DTO.Tender
{
    using IntegrationLibrary.Tender;
    using IntegrationLibrary.Tender.Enums;
    using System;
    using System.Collections.Generic;

    public class GetTenderDTO
    {
        public int Id { get; set; }
        public TenderStatus Status { get; set; }
        public DateTime DueDate { get; set; }
        public List<ViewTenderOfferDTO> Offers { get; set; }
        public ViewTenderOfferDTO TenderWinner { get; set; }
        public List<ViewTenderItemDTO> Items { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
