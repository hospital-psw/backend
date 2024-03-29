﻿namespace IntegrationLibrary.Tender.Interfaces
{
    using IntegrationLibrary.Core;
    using System;
    using System.Collections.Generic;

    public interface ITenderService : IService<Tender>
    {
        TenderOffer MakeAnOffer(int tenderId, TenderOffer offer);
        List<Tender> GetActive();
        List<Tender> GetClosed();
        double AvgTotalPrice();
        double WinningOfferPrice();

        void FinishTender(int tenderId, int offerIndex);
        List<double> GetMoneyPerMonth(int year);
        List<double> GetBloodPerMonth(int year, int bloodType);
    }
}
