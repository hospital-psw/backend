namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model.Events.Scheduling.Root;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AppointmentSchedulingRootRepository : BaseRepository<AppointmentSchedulingRoot>, IAppointmentSchedulingRootRepository
    {
        public AppointmentSchedulingRootRepository(HospitalDbContext context) : base(context)
        {
        }

        public IEnumerable<AppointmentSchedulingRoot> GetByPatientId(int id)
        {
            return HospitalDbContext.AppointmentRoots.Where(x => x.PatientId == id && x.Completed == false);
        }


    }
}
