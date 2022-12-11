namespace HospitalLibrary.Core.Service.AppUsers.Core
{
    using HospitalLibrary.Core.Model.ApplicationUser;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IApplicationPatientService
    {
        ApplicationPatient Get(int id);
        IEnumerable<ApplicationPatient> GetAll();
        IEnumerable<ApplicationPatient> GetNonHospitalized();
        IEnumerable<ApplicationPatient> GetBlocked();
        IEnumerable<ApplicationPatient> GetMalicious();
        Task<ApplicationPatient> BlockPatient(int id);
        Task<ApplicationPatient> UnblockPatient(int id);
        Task<ApplicationPatient> SetStrikes(int id, int num);
    }
}
