namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.DTO.VacationRequest;
    using HospitalLibrary.Core.Model.VacationRequest;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IVacationRequestsService
    {
        IEnumerable<VacationRequest> GetAllPending();

        VacationRequest Create(NewVacationRequestDto dto);
    }
}
