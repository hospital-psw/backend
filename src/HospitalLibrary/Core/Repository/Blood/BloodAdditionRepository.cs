namespace HospitalLibrary.Core.Repository.Blood
{
    using HospitalLibrary.Core.Model.Blood;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Repository.Blood.Core;
    using HospitalLibrary.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BloodAdditionRepository : BaseRepository<BloodAddition>, IBloodAdditionRepository
    {

        public BloodAdditionRepository(HospitalDbContext context) : base(context)
        {
        }

        public IEnumerable<BloodAddition> GetByBloodType(BloodType bloodType)
        {
            return HospitalDbContext.BloodAdditions.Where(x => x.BloodType == bloodType);
        }

    }
}
