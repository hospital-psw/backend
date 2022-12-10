namespace HospitalLibrary.Core.Repository.AppUsers
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Repository.AppUsers.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ApplicationPatientRepository : BaseRepository<ApplicationPatient>, IApplicationPatientRepository
    {
        public ApplicationPatientRepository(HospitalDbContext context) : base(context) { }

        public override IEnumerable<ApplicationPatient> GetAll()
        {
            return HospitalDbContext.ApplicationPatients.Include(x => x.applicationDoctor)
                                               .Include(x => x.Allergies)
                                               .ToList();
        }

        public override ApplicationPatient Get(int id)
        {
            return GetAll().Where(x => x.Id == id)
                           .FirstOrDefault();
        }

        public IEnumerable<ApplicationPatient> GetNonHospitalized()
        {
            return GetAll().Where(x => !x.Hospitalized)
                           .ToList();
        }

        IEnumerable<ApplicationPatient> IApplicationPatientRepository.GetBlocked()
        {
            return GetAll().Where(x => x.Blocked)
                           .ToList();
        }

        IEnumerable<ApplicationPatient> IApplicationPatientRepository.GetMalicious()
        {
            return GetAll().Where(x => x.Strikes > 2)
                           .ToList();
        }
    }
}
