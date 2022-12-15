namespace IntegrationAPI.DTO.Tender
{
    using IntegrationLibrary.Tender;
    using IntegrationLibrary.Tender.Enums;

    public class ViewTenderItemDTO
    {
        public BloodType BloodType { get; set; }
        public Money Money { get; set; }
        public double Quantity { get; set; }
    }
}
