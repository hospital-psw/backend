using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Repository.Core;
using IntegrationLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        public IntegrationDbContext HospitalDbContext
        {
            get { return _context as IntegrationDbContext; }
        }

        public BaseRepository(IntegrationDbContext context)
        {
            _context = context;
        }

        public virtual TEntity Get(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().Where(x => !(x as Entity).Deleted).ToList();
        }

        public virtual void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Entry(entity).State = (entity as Entity).Id == 0 ? EntityState.Added : EntityState.Modified;
        }
    }
}