namespace IntegrationLibrary.Tender
{
    using IntegrationLibrary.Core;
    using IntegrationLibrary.Tender.Enums;

    public class TenderItem : Entity
    {
        public BloodType BloodType { get; set; }
        public Money Money { get; set; }
        public double Quantity { get; set; }

        public virtual bool Equals(TenderItem other)
        {
            return this.BloodType.Equals(other.BloodType) && this.Money.Equals(other.Money) && this.Quantity == other.Quantity;
        }

        override public int GetHashCode()
        {
            return this.BloodType.GetHashCode() + this.Money.GetHashCode() + this.Quantity.GetHashCode();
        }
    }
}
