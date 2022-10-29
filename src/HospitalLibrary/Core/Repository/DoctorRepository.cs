namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model;
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
            return HospitalDbContext.Doctors.Include(x => x.Office)
                                            .Include(x => x.WorkHours)
                                            .FirstOrDefault(x => x.Id == id);
        }
    }
}
   

