namespace HospitalLibrary.Core.Repository.Core
{
    using HospitalLibrary.Core.Model.MedicalTreatment;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMedicalTreatmentRepository : IBaseRepository<MedicalTreatment>
    {
        IEnumerable<MedicalTreatment> GetActive();
        IEnumerable<MedicalTreatment> GetInactive();
    }
}
