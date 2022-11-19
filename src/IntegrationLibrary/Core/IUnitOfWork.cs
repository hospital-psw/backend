namespace IntegrationLibrary.Core
{
    using IntegrationLibrary.BloodBank.Interfaces;
    using IntegrationLibrary.News.Interfaces;
    using System;

    public interface IUnitOfWork : IDisposable
    {
        public IBloodBankRepository BloodBankRepository { get; }
        public INewsRepository NewsRepository { get; }
        int Save();
    }
}
