namespace HospitalLibrary.Core.Repository.Examinations.Core
{
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Repository.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISymptomRepository : IBaseRepository<Symptom>
    {
        Symptom GetByName(string name);

        IEnumerable<Symptom> GetSelectedSymptoms(List<int> ids);
    }
}
