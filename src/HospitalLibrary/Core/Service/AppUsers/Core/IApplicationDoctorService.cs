namespace HospitalLibrary.Core.Service.AppUsers.Core
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IApplicationDoctorService
    {
        ApplicationDoctor Get(int id);
        IEnumerable<ApplicationDoctor> GetAll();
        IEnumerable<ApplicationDoctor> GetBySpecialization(Specialization specialization);
        IEnumerable<ApplicationDoctor> RecommendDoctors();
        IEnumerable<ApplicationDoctor> GetDoctorsWhoWorksInSameShift(int workHourId);
        IEnumerable<Specialization> GetSpecializationsOfDoctorsWhoWorksInSameShift(int workHourId);
        bool ChangeDoctorsShift(WorkingHours newWorkingHours, int id);
    }
}
