namespace HospitalLibrary.Core.Repository.Blood
{
    using HospitalLibrary.Core.Model.Blood.BloodManagment;
    using HospitalLibrary.Core.Repository.Blood.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
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
            return HospitalDbContext.BloodAcquisitions.Include(x => x.Doctor);
        }


        public override BloodAcquisition Get(int id)
        {
            return HospitalDbContext.BloodAcquisitions.Include(x => x.Doctor)
                                                      .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<BloodAcquisition> GetPendingAcquisitions()
        {
            return HospitalDbContext.BloodAcquisitions.Include(x => x.Doctor)
                                                       .Where(x => x.Status == Model.Blood.Enums.BloodRequestStatus.PENDING);
        }

        public IEnumerable<BloodAcquisition> GetAcquisitionsForSpecificDoctor(int id)
        {
            return HospitalDbContext.BloodAcquisitions.Include(x => x.Doctor)
                                                      .Where(x => x.Doctor.Id == id);
        }

    }
}
