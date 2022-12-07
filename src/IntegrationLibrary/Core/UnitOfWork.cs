﻿using IntegrationLibrary.BloodBank;

namespace IntegrationLibrary.Core
{
    using IntegrationLibrary.BloodBank.Interfaces;
    using IntegrationLibrary.News;
    using IntegrationLibrary.News.Interfaces;
    using IntegrationLibrary.Notification;
    using IntegrationLibrary.Notification.Interfaces;
    using IntegrationLibrary.Settings;
    using IntegrationLibrary.Tender;
    using IntegrationLibrary.Tender.Interfaces;

    public class UnitOfWork : IUnitOfWork
    {

        private readonly IntegrationDbContext _context;
        public IBloodBankRepository BloodBankRepository { get; }
        public INewsRepository NewsRepository { get; }

        public ITenderRepository TenderRepository { get; }
        public INotificationRepository NotificationRepository { get; }

        public UnitOfWork(IntegrationDbContext context)
        {
            _context = context;

            BloodBankRepository = new BloodBankRepository(_context);
            NewsRepository = new NewsRepository(_context);
            TenderRepository = new TenderRepository(_context);
            NotificationRepository = new NotificationRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
