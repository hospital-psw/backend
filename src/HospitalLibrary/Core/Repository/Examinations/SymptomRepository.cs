namespace HospitalLibrary.Core.Repository.Examinations
{
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Repository.Examinations.Core;
    using HospitalLibrary.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SymptomRepository : BaseRepository<Symptom>, ISymptomRepository
    {
        public SymptomRepository(HospitalDbContext context) : base(context)
        {
        }

        public Symptom GetByName(string name)
        {
            return HospitalDbContext.Symptoms.Where(x => x.Name == name).FirstOrDefault();
        }

        public IEnumerable<Symptom> GetSelectedSymptoms(List<int> ids)
        {
            return HospitalDbContext.Symptoms.Where(x => ids.Contains(x.Id));
        }
    }
}
