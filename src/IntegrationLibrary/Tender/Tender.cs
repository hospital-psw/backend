using IntegrationLibrary.Core;
using IntegrationLibrary.Tender.Enums;
using System;
using System.Collections.Generic;


namespace IntegrationLibrary.Tender

{
    using BloodBank;
    using Mailjet.Client.Resources.SMS;
    using static System.Net.WebRequestMethods;

    public class Tender : Entity
    {
        public TenderStatus Status { get; set; }
        public DateTime DueDate { get; set; }
        public List<TenderOffer> Offers { get; set; }
        public TenderOffer TenderWinner { get; set; }
        public List<TenderItem> Items { get; set; }
        public BloodBank Sender { get; set; }

        public Money TotalSum()
        {
            Money totalSum = new Money();

            foreach (TenderItem i in Items)
            {
                totalSum.Add(i.Money);
            }
            return totalSum;
        }

        public TenderOffer MakeAnOffer(TenderOffer offer)
        {
            if (offer.Offeror == null || offer.Items == null || offer.Items.Count == 0 || Status == TenderStatus.CLOSED || !OfferMatchesTenderSpec(offer))
            {
                return null;
            }
            if (Offers == null)
            {
                Offers = new List<TenderOffer>();
            }
            Offers.Add(offer);
            return offer;
        }

        private bool OfferMatchesTenderSpec(TenderOffer offer)
        {
            if (offer == null)
            {
                return false;
            }
            foreach (TenderItem item in Items)
            {
                if (offer.Items.Find(x => x.BloodType == item.BloodType && item.Quantity <= x.Quantity) == null)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
