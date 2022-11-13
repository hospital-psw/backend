namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RelocationRepository : BaseRepository<RelocationRequest>, IRelocationRepository
    {
        private HospitalDbContext _context;
        public RelocationRepository(HospitalDbContext context) : base(context)
        {
            _context = context;
        }

        public RelocationRequest Create(RelocationRequest relocationRequest)
        {
            _context.RelocationRequests.Add(relocationRequest);
            HospitalDbContext.SaveChanges();
            return relocationRequest;
        } 
    }
}
