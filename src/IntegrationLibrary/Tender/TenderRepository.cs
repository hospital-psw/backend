using IntegrationLibrary.Settings;
using IntegrationLibrary.Tender.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Tender
{


    public class TenderRepository : ITenderRepository
    {
        private readonly DbContext _context;

        public TenderRepository(IntegrationDbContext context)
        {
            _context = context;
        }

        public IntegrationDbContext IntegrationDbContext
        {
            get { return _context as IntegrationDbContext; }
        }

        public Tender Get(int id)
        {
            return GetAll().Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Tender> GetAll()
        {
            return _context.Set<Tender>()
                .Include(x => x.Items)
                .Where(x => !x.Deleted).ToList();
        }

        public void Add(Tender entity)
        {
            _context.Set<Tender>().Add(entity);
        }

        public void Update(Tender entity)
        {
            _context.Entry(entity).State = entity.Id == 0 ? EntityState.Added : EntityState.Modified;
        }

    }
}
