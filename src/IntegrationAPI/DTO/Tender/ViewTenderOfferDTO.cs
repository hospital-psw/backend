namespace IntegrationAPI.DTO.Tender
{
    using IntegrationAPI.DTO.BloodBank;
    using IntegrationLibrary.Tender;
    using System;
    using System.Collections.Generic;

    public class ViewTenderOfferDTO
    {
        public int Id { get; set; }
        public GetBloodBankDTO Offeror { get; set; }
        public DateTime DateCreated { get; set; }
        public List<ViewTenderItemDTO> Items { get; set; }
    }
}
