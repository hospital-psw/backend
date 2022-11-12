using IntegrationLibrary.BloodBank;

namespace IntegrationLibrary.Core
{
    using IntegrationLibrary.BloodBank.Interfaces;
    using IntegrationLibrary.Settings;

    public class UnitOfWork : IUnitOfWork
    {

        private readonly IntegrationDbContext _context;

        public IBloodBankRepository BloodBankRepository { get; }

        public UnitOfWork(IntegrationDbContext context)
        {
            _context = context;

            BloodBankRepository = new BloodBankRepository(_context);
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
