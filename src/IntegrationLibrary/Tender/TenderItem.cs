namespace IntegrationLibrary.Tender
{
    using IntegrationLibrary.Core;
    using IntegrationLibrary.Tender.Enums;

    public class TenderItem : Entity
    {
        public BloodType BloodType { get; set; }
        public Money Money { get; set; }
        public double Quantity { get; set; }
    }
}
