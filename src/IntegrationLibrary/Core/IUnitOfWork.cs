﻿namespace IntegrationLibrary.Core
{
    using IntegrationLibrary.BloodBank.Interfaces;
    using IntegrationLibrary.News.Interfaces;
    using IntegrationLibrary.Notification.Interfaces;
    using IntegrationLibrary.Tender.Interfaces;
    using System;

    public interface IUnitOfWork : IDisposable
    {
        public IBloodBankRepository BloodBankRepository { get; }
        public INewsRepository NewsRepository { get; }
        public ITenderRepository TenderRepository { get; }
        public INotificationRepository NotificationRepository { get; }
        int Save();
    }
}
