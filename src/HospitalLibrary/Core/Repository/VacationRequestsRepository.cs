namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model.VacationRequest;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class VacationRequestsRepository : BaseRepository<VacationRequest>, IVacationRequestsRepository
    {
        private readonly HospitalDbContext _context;
        public VacationRequestsRepository(HospitalDbContext context) : base(context)
        { 
            _context = context;
        }

        public IEnumerable<VacationRequest> GetAllPending()
        {
            return _context.VacationRequests.Include(x => x.Doctor).
                                            Where(x => x.Status == 0).Where(x => !x.Deleted).
                                            ToList();
        }
        
    }
}
