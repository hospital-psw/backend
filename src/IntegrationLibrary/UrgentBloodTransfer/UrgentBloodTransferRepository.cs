namespace IntegrationLibrary.UrgentBloodTransfer
{
    using IntegrationLibrary.Settings;
    using IntegrationLibrary.UrgentBloodTransfer.Interfaces;
    using IntegrationLibrary.UrgentBloodTransfer.Model;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UrgentBloodTransferRepository : IUrgentBloodTransferRepository
    {
        private readonly DbContext _context;

        public UrgentBloodTransferRepository(IntegrationDbContext context)
        {
            _context = context;
        }

        public IntegrationDbContext IntegrationDbContext
        {
            get { return _context as IntegrationDbContext; }
        }

        public void Add(UrgentBloodTransfer entity)
        {
            _context.Set<UrgentBloodTransfer>().Add(entity);
        }

        public void Delete(UrgentBloodTransfer entity)
        {
            _context.Remove(entity);
        }

        public UrgentBloodTransfer Get(int id)
        {
            throw new NotImplementedException();
        }

        public UrgentBloodTransfer Get(UrgentBloodTransfer entity)
        {
            return _context.Set<UrgentBloodTransfer>().Where(x => x == entity).Include(x => x.Sender).FirstOrDefault();
        }

        public IEnumerable<UrgentBloodTransfer> GetAll()
        {
            return _context.Set<UrgentBloodTransfer>().Include(x => x.Sender).ToList();
        }

        public void Update(UrgentBloodTransfer entity)
        {
            throw new NotImplementedException();
        }
    }
}
