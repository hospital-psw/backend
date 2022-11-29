namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;

    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(HospitalDbContext context) : base(context) { }
        public override Doctor Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public override IEnumerable<Doctor> GetAll()
        {
            return HospitalDbContext.Doctors.Include(x => x.Office)
                                            .Include(x => x.WorkHours)
                                            .Include(x => x.DoctorSchedule)
                                            .ToList();
        }

        public IEnumerable<Doctor> GetBySpecialization(Specialization specialization)
        {
            return GetAll().Where(x => x.Specialization == specialization).ToList();
        }

        public IEnumerable<Doctor> GetOtherSpecializationDoctors(Specialization specialization, int doctorId)
        {
            return GetBySpecialization(specialization).Where(x => x.Id != doctorId).ToList();
        }
    }
}


