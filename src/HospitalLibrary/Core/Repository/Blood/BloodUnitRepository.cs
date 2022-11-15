namespace HospitalLibrary.Core.Repository.Blood
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Blood;
    using HospitalLibrary.Core.Model.Blood.Enums;
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

        public int GetAmountForSpecificBloodType(BloodType bloodType)
        {
            BloodUnit bloodUnit = HospitalDbContext.BloodUnits.FirstOrDefault(u => u.BloodType == bloodType);
            return bloodUnit.Amount;
        }

        public BloodUnit GetByBloodType(BloodType bloodType)
        {
            return HospitalDbContext.BloodUnits.FirstOrDefault(u => u.BloodType.Equals(bloodType));
        }
    }
}
