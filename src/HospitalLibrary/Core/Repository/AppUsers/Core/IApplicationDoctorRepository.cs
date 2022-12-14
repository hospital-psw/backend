namespace HospitalLibrary.Core.Repository.AppUsers.Core
{
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Repository.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IApplicationDoctorRepository : IBaseRepository<ApplicationDoctor>
    {
        IEnumerable<ApplicationDoctor> GetBySpecialization(Specialization specialization);
        IEnumerable<ApplicationDoctor> GetOtherSpecializationDoctors(Specialization specialization, int doctorId);
        IEnumerable<ApplicationDoctor> GetSelectedDoctors(List<int> doctorsIds);
        IEnumerable<ApplicationDoctor> GetDoctorsWhoWorksInSameShift(int workHourId);
        IEnumerable<ApplicationDoctor> GetDoctorsOfSelectedSpecializations(List<Specialization> specializations, int workHourId);
        IEnumerable<Specialization> GetSpecializationsOfDoctorsWhoWorksInSameShift(int workHourId);
    }
}
