namespace HospitalLibrary.Core.Repository.Examinations.Core
{
    using HospitalLibrary.Core.Model.Domain;
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Repository.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAnamnesisRepository : IBaseRepository<Anamnesis>
    {
        IEnumerable<Anamnesis> GetByPatient(int patientId);

        IEnumerable<Anamnesis> GetByDoctor(int doctorId);

        IEnumerable<Anamnesis> GetInDateRange(DateRange dateRange);
    }
}
