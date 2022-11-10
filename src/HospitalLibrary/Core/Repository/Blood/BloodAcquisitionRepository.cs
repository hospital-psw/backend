namespace HospitalLibrary.Core.Repository.Blood
{
    using HospitalLibrary.Core.Model.Blood.BloodManagment;
    using HospitalLibrary.Core.Repository.Blood.Core;
    using HospitalLibrary.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BloodAcquisitionRepository : BaseRepository<BloodAcquisition>, IBloodAcquisitionRepository
    {  
        public BloodAcquisitionRepository(HospitalDbContext context) : base(context)
        {
        }

        public override IEnumerable<BloodAcquisition> GetAll()
        {
            return base.GetAll();
        }


        public override BloodAcquisition Get(int id)
        {
            return base.Get(id);
        }

    }
}
