namespace IntegrationLibrary.Tender.Interfaces
{
    using IntegrationLibrary.Core;
    using System;

    public interface ITenderService : IService<Tender>
    {
        TenderOffer MakeAnOffer(int tenderId, TenderOffer offer);
        Tender GetActive();
        double AvgTotalPrice();
        double WinningOfferPrice();
    }
}
