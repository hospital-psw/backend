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

        public override Patient Get(int id)
        {
            return GetAll().Where(x => x.Id == id)
                            .FirstOrDefault();
        }

        public override IEnumerable<Patient> GetAll()
        {
            return HospitalDbContext.Patients.Include(x => x.Allergies)
                                             .Where(x => !x.Deleted)
                                             .ToList();
        }

        public IEnumerable<Patient> GetNonHospitalized()
        {
            return GetAll().Where(x => !x.Hospitalized)
                                             .ToList();
        }
    }
}
