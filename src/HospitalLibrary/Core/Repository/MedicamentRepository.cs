namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model.Medicament;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MedicamentRepository : BaseRepository<Medicament>, IMedicamentRepository
    {
        public MedicamentRepository(HospitalDbContext context) : base(context)
        {
        }

    }
}
