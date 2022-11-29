namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IApplicationPatientService
    {
        ApplicationPatient GetPatient(int patientId);
    }
}
