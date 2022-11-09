namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model.Therapy;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
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
    }
}
