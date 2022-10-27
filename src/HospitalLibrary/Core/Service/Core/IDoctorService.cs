namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.DTO;
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDoctorService
    {
        Doctor Add(Doctor doctor);
        Doctor Update(Doctor doctor);
        Doctor Get(int doctorId);
        IEnumerable<Doctor> GetAll();
        bool Delete(int doctorId);

    }
}
