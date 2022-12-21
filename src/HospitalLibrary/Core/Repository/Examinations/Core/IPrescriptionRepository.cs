namespace HospitalLibrary.Core.Repository.Examinations.Core
{
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Repository.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IPrescriptionRepository : IBaseRepository<Prescription>
    {
        IEnumerable<Prescription> GetAnamnesesBySearchCriteria(string criteria);
    }
}
