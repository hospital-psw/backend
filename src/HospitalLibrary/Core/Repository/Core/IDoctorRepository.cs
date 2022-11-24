namespace HospitalLibrary.Core.Repository.Core
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDoctorRepository : IBaseRepository<Doctor>
    {
        IEnumerable<Doctor> GetBySpecialization(Specialization specialization);
        IEnumerable<Doctor> GetOtherSpecializationDoctors(Specialization specialization, int doctorId);
    }
}
