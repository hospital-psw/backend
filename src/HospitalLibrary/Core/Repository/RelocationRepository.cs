namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
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

        public List<RelocationRequest> GetScheduledRelocationsForRoom(int roomId)
        {
            return HospitalDbContext.RelocationRequests.Include(x => x.FromRoom)
                                               .Include(x => x.ToRoom)
                                               .Include(x => x.Equipment)
                                               .Where(x => !x.Deleted && (x.FromRoom.Id == roomId || x.ToRoom.Id == roomId))
                                               .OrderBy(x => x.StartTime)
                                               .Distinct()
                                               .ToList();
        }

        public List<RelocationRequest> GetFinishedRelocations() {
            return HospitalDbContext.RelocationRequests.Include(x => x.FromRoom)
                                                        .Include(x => x.ToRoom)
                                                        .Include(x => x.Equipment)
                                                        .Where(x => !x.Deleted && x.StartTime.AddHours(x.Duration) <= DateTime.Now)
                                                        .ToList();
        }
        public int Save()
        {
            return _context.SaveChanges();
        }

    }
}
