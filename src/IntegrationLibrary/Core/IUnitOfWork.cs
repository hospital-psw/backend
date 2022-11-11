namespace IntegrationLibrary.Core
{
    using IntegrationLibrary.BloodBank.Interfaces;
    using System;

    public interface IUnitOfWork : IDisposable
    {
        public IBloodBankRepository BloodBankRepository { get; }
        int Save();
    }
}
