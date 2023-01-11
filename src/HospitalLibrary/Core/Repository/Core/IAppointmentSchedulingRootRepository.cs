namespace HospitalLibrary.Core.Repository.Core
{
    using HospitalLibrary.Core.Model.Events.Scheduling.Root;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAppointmentSchedulingRootRepository : IBaseRepository<AppointmentSchedulingRoot>
    {
        public IEnumerable<AppointmentSchedulingRoot> GetByPatientId(int id);
    }
}
