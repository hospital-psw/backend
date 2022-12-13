namespace HospitalLibrary.Core.Repository.Core
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDoctorScheduleRepository : IBaseRepository<DoctorSchedule>
    {

        IEnumerable<DoctorSchedule> GetDoctorSchedulesByDoctorList(List<ApplicationDoctor> doctorList);
        DoctorSchedule GetDoctorScheduleByDoctorId(int doctorId);
    }
}
