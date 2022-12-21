namespace IntegrationLibrary.Tender.Interfaces
{
    using IntegrationLibrary.Core;
    using System;
    using System.Collections.Generic;

    public interface ITenderService : IService<Tender>
    {
        TenderOffer MakeAnOffer(int tenderId, TenderOffer offer);
        List<Tender> GetActive();
        double AvgTotalPrice();
        double WinningOfferPrice();

        void FinishTender(int tenderId, int offerIndex);
    }
}
