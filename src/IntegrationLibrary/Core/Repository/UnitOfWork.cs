namespace IntegrationLibrary.Core.Repository
{
    using HospitalLibrary.Core.Repository.Core;
    using IntegrationLibrary.Core.Repository.Core;
    using IntegrationLibrary.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork
    {

        private readonly IntegrationDbContext _context;
        private Dictionary<string, dynamic> _repositories;

        public UnitOfWork(IntegrationDbContext context)
        {
            _context = context;

            BloodBankRepository = new BloodBankRepository(_context);
        }

        public IBloodBankRepository BloodBankRepository { get; set; }


        public IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            string type = typeof(TEntity).Name;

            if (_repositories == null)
            {
                _repositories = new Dictionary<string, dynamic>();
                Type repositoryType = typeof(BaseRepository<>);
                _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context));
                return (IBaseRepository<TEntity>)_repositories[type];

            }
            else if (_repositories.ContainsKey(type))
            {
                return (IBaseRepository<TEntity>)_repositories[type];
            }

            return null;
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