namespace HospitalLibrary.Core.Service.Examinations.Core
{
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Service.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISymptomService : IBaseService<Symptom>
    {
        Symptom GetByName(string name);
    }
}
