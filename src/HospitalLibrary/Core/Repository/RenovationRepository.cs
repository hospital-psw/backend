namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;

    public class RenovationRepository : BaseRepository<RenovationRequest>, IRenovationRepository
    {
        private HospitalDbContext _context;
        public RenovationRepository(HospitalDbContext context) : base(context)
        {
            _context = context;
        }

        public RenovationRequest Create(RenovationRequest renovationRequest)
        {
            _context.RenovationRequests.Add(renovationRequest);
            HospitalDbContext.SaveChanges();
            return renovationRequest;
        }

        public override List<RenovationRequest> GetAll()
        {
            return HospitalDbContext.RenovationRequests.Include(x => x.Rooms)
                                                        .Include(x => x.RenovationDetails)
                                                        .Where(x => !x.Deleted)
                                                        .OrderBy(x => x.StartTime)
                                                        .Distinct()
                                                        .ToList();
        }

        public List<RenovationRequest> GetScheduledRenovationsForRoom(int roomId)
        {
            List<RenovationRequest> roomRenovations = new();
            foreach(var request in GetAll())
            {
                foreach (var room in request.Rooms)
                {
                    if (room.Id == roomId) roomRenovations.Add(request);
                }
            }
            return roomRenovations;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
