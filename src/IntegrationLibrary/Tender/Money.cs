namespace IntegrationLibrary.Tender
{
    using IntegrationLibrary.Tender.Enums;
    using Microsoft.EntityFrameworkCore;
    [Owned]
    public class Money
    {
        public double Amount { get; set; }
        public Currency Currency { get; set; }

        public Money Add(Money money)
        {
            money.Currency = Currency.EUR;
            money.Amount += Amount;
            return money;
        }

        public virtual bool Equals(Money other)
        {
            return this.Currency.Equals(other.Currency) && this.Amount == other.Amount;
        }

        public virtual int GetHashCode()
        {
            return this.Currency.GetHashCode() + this.Amount;
        }
    }
}
