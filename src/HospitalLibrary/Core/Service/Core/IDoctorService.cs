namespace HospitalLibrary.Core.Service.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HospitalLibrary.Core.Model;

    public interface IDoctorService
    {
        Doctor Add(Doctor doctor);
        Doctor Update(Doctor doctor);
        Doctor Get(int doctorId);
        IEnumerable<Doctor> GetAll();
        bool Delete(int doctorId);

    }
}