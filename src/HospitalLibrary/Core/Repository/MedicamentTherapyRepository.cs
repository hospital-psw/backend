namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model.Therapy;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MedicamentTherapyRepository : BaseRepository<MedicamentTherapy>, IMedicamentTherapyRepository
    {
        public MedicamentTherapyRepository(HospitalDbContext context) : base(context)
        {
        }

        public override MedicamentTherapy Get(int id)
        {
            return HospitalDbContext.MedicamentTherapies.Include(x => x.Medicament)
                                                        .FirstOrDefault(x => x.Id == id);
        }

        public override IEnumerable<MedicamentTherapy> GetAll()
        {
            return HospitalDbContext.MedicamentTherapies.Include(x => x.Medicament)
                                                        .ToList();
        }
    }
}
