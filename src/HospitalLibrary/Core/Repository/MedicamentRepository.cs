namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Medicament;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
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

        public override IEnumerable<Medicament> GetAll()
        {
            return HospitalDbContext.Medicaments.Include(x => x.Allergens)
                                                .Where(x => !x.Deleted)
                                                .ToList();
        }

        public IEnumerable<Medicament> GetAcceptableMedicaments(Patient patient)
        {
            return HospitalDbContext.Medicaments.Include(x => x.Allergens)
                                                .Where(x => x.Allergens.All(al => !patient.Allergies.Contains(al)))
                                                .ToList();
        }
    }
}
