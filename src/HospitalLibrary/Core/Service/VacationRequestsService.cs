namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.VacationRequest;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Settings;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class VacationRequestsService : BaseService<VacationRequest>, IVacationRequestsService
    {
        private readonly ILogger<VacationRequest> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public VacationRequestsService(ILogger<VacationRequest> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<VacationRequest> GetAllPending()
        {
            try
            {
                return _unitOfWork.VacationRequestsRepository.GetAllPending();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void HandleVacationRequest(VacationRequestStatus status, int id, string managerComment) 
        {
            VacationRequest request = _unitOfWork.VacationRequestsRepository.Get(id);
            request.Status = status;
            request.ManagerComment = managerComment;
            _unitOfWork.VacationRequestsRepository.Update(request);
            _unitOfWork.VacationRequestsRepository.Save();
        }
    }

}
