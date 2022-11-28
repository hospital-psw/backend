namespace HospitalLibrary.Core.Repository.AppUsers.Core
{
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Repository.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IApplicationPatientRepository : IBaseRepository<ApplicationPatient>
    {
        IEnumerable<ApplicationPatient> GetNonHospitalized();
    }
}
