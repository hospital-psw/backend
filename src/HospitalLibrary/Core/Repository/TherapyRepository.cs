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

    public class TherapyRepository : BaseRepository<Therapy>, ITherapyRepository
    {
        public TherapyRepository(HospitalDbContext context) : base(context)
        {
        }
    }
}
