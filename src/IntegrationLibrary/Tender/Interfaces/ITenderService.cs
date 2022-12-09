namespace IntegrationLibrary.Tender.Interfaces
{
    using IntegrationLibrary.Core;
    using System;

    public interface ITenderService : IService<Tender>
    {
        double AvgTotalPrice();
        double WinningOfferPrice();
    }
}
