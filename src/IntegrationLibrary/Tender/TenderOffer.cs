using IntegrationLibrary.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Tender
{
    using IntegrationLibrary.BloodBank;

    public class TenderOffer : Entity
    {
        public BloodBank Offeror { get; set; }
        public List<TenderItem> Items { get; set; }

        public Money TotalSum()
        {
            Money totalSum = new Money();
            foreach(TenderItem item in Items)
            {
                totalSum.Add(item.Money);
            }
            return totalSum;
        }
    }
}
