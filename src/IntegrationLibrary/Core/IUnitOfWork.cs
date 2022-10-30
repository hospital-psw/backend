namespace IntegrationLibrary.Core
{
    using System;

    public interface IUnitOfWork : IDisposable
    {
        int Save();
    }
}
