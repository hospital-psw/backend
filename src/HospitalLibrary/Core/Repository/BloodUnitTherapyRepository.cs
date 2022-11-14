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

    public class BloodUnitTherapyRepository : BaseRepository<BloodUnitTherapy>, IBloodUnitTherapyRepository
    {
        public BloodUnitTherapyRepository(HospitalDbContext context) : base(context)
        {
        }

        public override BloodUnitTherapy Get(int id)
        {
            return HospitalDbContext.BloodUnitTherapies.Include(x => x.BloodUnit)
                                                       .FirstOrDefault(x => x.Id == id && !x.Deleted);       
        }

        public override IEnumerable<BloodUnitTherapy> GetAll()
        {
            return HospitalDbContext.BloodUnitTherapies.Include(x => x.BloodUnit)
                                .Where(x => !x.Deleted)
                                .ToList();
        }
    }
}
