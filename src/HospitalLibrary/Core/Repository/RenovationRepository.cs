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

        List<RenovationRequest> IRenovationRepository.GetAll()
        {
            /*return _context.RenovationRequests.Include(x => x.Rooms)
                                              .Where(x => !x.Deleted)
                                               .ToList();  */
            return null;
        }
    }
}
