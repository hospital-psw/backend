using IntegrationLibrary.BloodBank;

namespace IntegrationLibrary.Core
{
    using IntegrationLibrary.BloodBank.Interfaces;
    using IntegrationLibrary.News;
    using IntegrationLibrary.News.Interfaces;
    using IntegrationLibrary.Settings;
    using IntegrationLibrary.Tender;
    using IntegrationLibrary.Tender.Interfaces;
    using IntegrationLibrary.UrgentBloodTransfer;
    using IntegrationLibrary.UrgentBloodTransfer.Interfaces;

    public class UnitOfWork : IUnitOfWork
    {

        private readonly IntegrationDbContext _context;
        public IBloodBankRepository BloodBankRepository { get; }
        public INewsRepository NewsRepository { get; }
        public ITenderRepository TenderRepository { get; }
        public IUrgentBloodTransferRepository UrgentBloodTransferRepository { get; }

        public UnitOfWork(IntegrationDbContext context)
        {
            _context = context;

            BloodBankRepository = new BloodBankRepository(_context);
            NewsRepository = new NewsRepository(_context);
            TenderRepository = new TenderRepository(_context);
            UrgentBloodTransferRepository = new UrgentBloodTransferRepository(_context);
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
