namespace HospitalLibrary.Core.Repository.Core
{
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.VacationRequest;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IVacationRequestsRepository : IBaseRepository<VacationRequest>
    {
        IEnumerable<VacationRequest> GetAllPending();
        int Save();
    }
}
