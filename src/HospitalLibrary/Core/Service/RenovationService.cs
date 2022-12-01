namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RenovationService : BaseService<RenovationRequest>, IRenovationService
    {
        public RenovationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public RenovationRequest Create(RenovationRequest entity)
        {
            try
            {
                return _unitOfWork.RenovationRepository.Create(entity);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<RenovationRequest> GetAllForRoom(int roomId)
        {
            List<RenovationRequest> futureRenovations = new List<RenovationRequest>();

            foreach(RenovationRequest renovationRequest in _unitOfWork.RenovationRepository.GetAll())
            {
                foreach(Room room in renovationRequest.Rooms)
                {
                    if (room.Id != roomId) continue;
                    if (renovationRequest.StartTime >= DateTime.Now) futureRenovations.Add(renovationRequest);
                }
            }
            return futureRenovations;
        }

        public void Decline(int requestId)
        {
            RenovationRequest request = _unitOfWork.RenovationRepository.Get(requestId);
            request.Deleted = true;
            _unitOfWork.RenovationRepository.Update(request);
            _unitOfWork.RenovationRepository.Save();
        }
    }
}
