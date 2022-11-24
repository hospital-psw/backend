namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ApplicationUserRepository : BaseRepository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly HospitalDbContext _context;
        public ApplicationUserRepository(HospitalDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationDoctor> GetAllDoctors()
        {
            return _context.ApplicationDoctors.ToList();
        }
        public IEnumerable<ApplicationDoctor> GetAllGeneralDoctors()
        {
            return GetAllDoctors().Where(x => x.Specialization == Specialization.GENERAL).ToList();
        }

        public IEnumerable<ApplicationPatient> GetAllPatients()
        {
            return _context.ApplicationPatients.ToList();
        }
    }
}
