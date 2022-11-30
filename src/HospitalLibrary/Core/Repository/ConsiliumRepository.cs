namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ConsiliumRepository : BaseRepository<Consilium>, IConsiliumRepository
    {
        public ConsiliumRepository(HospitalDbContext context) : base(context) { }

        public override Consilium Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public override IEnumerable<Consilium> GetAll()
        {
            return HospitalDbContext.Consiliums.Include(x => x.DoctorsSchedule)
                                               .Where(x => !x.Deleted)
                                               .ToList();
        }
    }
}
