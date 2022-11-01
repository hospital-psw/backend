using IntegrationLibrary.BloodBank.Interfaces;
using IntegrationLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationLibrary.BloodBank
{
    public class BloodBankRepository : IBloodBankRepository
    {
        private readonly DbContext _context;
        public BloodBankRepository(IntegrationDbContext context)
        {
            _context = context;
        }

        public IntegrationDbContext IntegrationDbContext
        {
            get { return _context as IntegrationDbContext; }
        }

        public BloodBank Get(int id)
        {
            return _context.Set<BloodBank>().Where(x => x.Id == id && !x.Deleted).FirstOrDefault();
        }

        public IEnumerable<BloodBank> GetAll()
        {
            return _context.Set<BloodBank>().Where(x => !x.Deleted).ToList();
        }

        public BloodBank GetByEmail(string email)
        {
            return _context.Set<BloodBank>().Where(x => x.Email.Equals(email) && !x.Deleted).FirstOrDefault();
        }

        public void Add(BloodBank entity)
        {
            _context.Set<BloodBank>().Add(entity);
        }

        public void Update(BloodBank entity)
        {
            _context.Entry(entity).State = entity.Id == 0 ? EntityState.Added : EntityState.Modified;
        }

    }
}
