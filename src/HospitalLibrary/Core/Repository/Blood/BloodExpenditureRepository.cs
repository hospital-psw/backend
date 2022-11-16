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

    public class BloodExpenditureRepository : BaseRepository<BloodExpenditure>, IBloodExpenditureRepository
    {
        public BloodExpenditureRepository(HospitalDbContext context) : base(context)
        {
        }


        public override BloodExpenditure Get(int id)
        {
            return HospitalDbContext.BloodExpenditures.Include(x => x.Doctor)
                                                      .FirstOrDefault(x => x.Id == id);
        }


        public override IEnumerable<BloodExpenditure> GetAll()
        {

            return HospitalDbContext.BloodExpenditures.Include(x => x.Doctor);


        }

    }
}
