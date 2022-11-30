namespace HospitalLibrary.Core.Repository.Examinations
{
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Repository.Examinations.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PrescriptionRepository : BaseRepository<Prescription>, IPrescriptionRepository
    {
        public PrescriptionRepository(HospitalDbContext context) : base(context)
        {
        }

        public override IEnumerable<Prescription> GetAll()
        {
            return HospitalDbContext.Prescriptions.Include(x => x.Medicament)
                                                  .Where(x => !x.Deleted);
        }

        public override Prescription Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
        
    }
}
