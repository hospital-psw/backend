namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.VacationRequest;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IVacationRequestsService
    {
        IEnumerable<VacationRequest> GetAllPending();

        void HandleVacationRequest(VacationRequestStatus status, int id, string managerComment);
    }
}
