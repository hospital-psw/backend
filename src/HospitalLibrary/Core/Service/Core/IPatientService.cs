namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IPatientService
    {
        Patient Add(Patient patient);
        Patient Update(Patient patient);
        Patient Get(int patientId);
        IEnumerable<Patient> GetAll();
        bool Delete(int patientId);
        IEnumerable<Patient> GetNonHospitalized();
    }
}
