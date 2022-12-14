using IntegrationLibrary.Settings;
using IntegrationLibrary.Tender.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
            return _context.Set<Tender>().Where(x => x.Id == id && !x.Deleted).Include(x => x.Items).Include(x => x.Offers).ThenInclude(x => x.Offeror).Include(x => x.Offers).ThenInclude(x => x.Items).FirstOrDefault();
        }

        public IEnumerable<Tender> GetAll()
        {
            return _context.Set<Tender>().Where(x => !x.Deleted).Include(x => x.Items).Include(x => x.Offers).ThenInclude(x => x.Offeror).Include(x => x.Offers).ThenInclude(x => x.Items).ToList();
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
