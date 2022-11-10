namespace HospitalLibrary.Core.Repository.Blood
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Blood;
    using HospitalLibrary.Core.Repository.Blood.Core;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BloodUnitRepository : BaseRepository<BloodUnit>, IBloodUnitRepository
    {
        public BloodUnitRepository(HospitalDbContext context) : base(context)
        {
        }


    }
}
