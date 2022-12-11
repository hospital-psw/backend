namespace HospitalLibrary.Core.Repository.AppUsers
{
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Repository.AppUsers.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ApplicationDoctorRepository : BaseRepository<ApplicationDoctor>, IApplicationDoctorRepository
    {
        public ApplicationDoctorRepository(HospitalDbContext context) : base(context) { }


        public override IEnumerable<ApplicationDoctor> GetAll()
        {
            return HospitalDbContext.ApplicationDoctors.Include(x => x.Office)
                                                       .Include(x => x.WorkHours);
        }

        public override ApplicationDoctor Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<ApplicationDoctor> GetBySpecialization(Specialization specialization)
        {
            return GetAll().Where(x => x.Specialization == specialization).ToList();
        }
        public IEnumerable<ApplicationDoctor> GetAllGeneralDoctors()
        {
            return GetAll().Where(x => x.Specialization == Specialization.GENERAL).ToList();
        }
        public IEnumerable<ApplicationDoctor> GetOtherSpecializationDoctors(Specialization specialization, int doctorId)
        {
            return GetBySpecialization(specialization).Where(x => x.Id != doctorId).ToList();
        }
    }
}
