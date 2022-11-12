namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model.VacationRequest;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class VacationRequestsRepository : BaseRepository<VacationRequest>, IVacationRequestsRepository
    {
        public VacationRequestsRepository(HospitalDbContext context) : base(context)
        {
        }
    }
}
