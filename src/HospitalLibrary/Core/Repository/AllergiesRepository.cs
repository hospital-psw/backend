namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Therapy;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AllergiesRepository : BaseRepository<Allergies>, IAllergiesRepository
    {
        private readonly HospitalDbContext _context;
        public AllergiesRepository(HospitalDbContext context) : base(context)
        {
            _context = context;
        }
        public override IEnumerable<Allergies> GetAll()
        {
            return _context.Allergies.ToList();
            
        }
    }
}
