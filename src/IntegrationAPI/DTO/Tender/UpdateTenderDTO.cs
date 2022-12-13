namespace IntegrationAPI.DTO.Tender
{
    using IntegrationLibrary.Tender.Enums;
    using System;

    public class UpdateTenderDTO
    {
        public int Id { get; set; }
        public TenderStatus Status { get; set; }
        public DateTime DueDate { get; set; }
    }
}
