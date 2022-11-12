namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(HospitalDbContext context) : base(context)
        {
        }

        public IEnumerable<Patient> GetNonHospitalized()
        {
            return HospitalDbContext.Patients.Where(x => !x.Hospitalized)
                                             .ToList();
        }
    }
}
