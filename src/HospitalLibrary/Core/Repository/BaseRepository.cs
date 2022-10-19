namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Metadata.Ecma335;
    using System.Text;
    using System.Threading.Tasks;

    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        public HospitalDbContext HospitalDbContext 
        {
            get { return _context as HospitalDbContext; }
        }

        public BaseRepository(HospitalDbContext context)
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

        public virtual void Remove(TEntity entity) 
        {
            _context.Set<TEntity>().Remove(entity);
        }
    }
}
