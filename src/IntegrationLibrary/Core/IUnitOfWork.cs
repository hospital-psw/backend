﻿namespace IntegrationLibrary.Core
{
    using IntegrationLibrary.BloodBank.Interfaces;
    using IntegrationLibrary.News.Interfaces;
    using IntegrationLibrary.Tender.Interfaces;
    using IntegrationLibrary.UrgentBloodTransfer.Interfaces;
    using System;

    public interface IUnitOfWork : IDisposable
    {
        public IBloodBankRepository BloodBankRepository { get; }
        public INewsRepository NewsRepository { get; }
        public ITenderRepository TenderRepository { get; }
        public IUrgentBloodTransferRepository UrgentBloodTransferRepository { get; }
        int Save();
    }
}
