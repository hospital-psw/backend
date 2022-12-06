namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.VacationRequests;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class RelocationService : BaseService<RelocationRequest>, IRelocationService
    {

        public RelocationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public RelocationRequest Create(RelocationRequest entity)
        {
            try
            {
                Equipment equipment = _unitOfWork.EquipmentRepository.Get(entity.Equipment.Id);
                equipment.ReservedQuantity += entity.Quantity;
                _unitOfWork.EquipmentRepository.Update(equipment);
                return _unitOfWork.RelocationRepository.Create(entity);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void FinishRelocation()
        {
            List<RelocationRequest> finished = _unitOfWork.RelocationRepository.GetFinishedRelocations();
            foreach (RelocationRequest request in finished)
            {
                RelocateEquipment(request);
            }
        }

        public void RelocateEquipment(RelocationRequest request)
        {
            Equipment equipment = _unitOfWork.EquipmentRepository.GetEquipment(request.Equipment.EquipmentType, request.ToRoom);
            if (equipment == null)
            {
                _unitOfWork.EquipmentRepository.Create(new Equipment(request.Equipment.EquipmentType, request.Quantity, request.ToRoom));
            }
            else
            {
                equipment.Quantity += request.Quantity;
                _unitOfWork.EquipmentRepository.Update(equipment);
                _unitOfWork.EquipmentRepository.Save();
            }
            SubtractEquipmentFromSourceRoom(request);

            request.Deleted = true;
            _unitOfWork.RelocationRepository.Update(request);
            _unitOfWork.RelocationRepository.Save();
        }

        private void SubtractEquipmentFromSourceRoom(RelocationRequest request)
        {
            request.Equipment.Quantity -= request.Quantity;
            if (request.Equipment.Quantity <= 0)
                request.Equipment.Deleted = true;
            request.Equipment.ReservedQuantity -= request.Quantity;
        }

        public List<RelocationRequest> GetAllForRoom(int roomId)
        {
            //.Where(x => x.StartTime >= DateTime.Now && x.Deleted==false)
            List<RelocationRequest> futureRequests = new List<RelocationRequest>();
            foreach (RelocationRequest relocationRequest in _unitOfWork.RelocationRepository.GetScheduledRelocationsForRoom(roomId))
            {
                if (relocationRequest.StartTime >= DateTime.Now && relocationRequest.Deleted == false) futureRequests.Add(relocationRequest);
            }
            return futureRequests;
        }

        public void Decline(int requestId)
        {
            RelocationRequest request = _unitOfWork.RelocationRepository.Get(requestId);
            request.DeleteRelocation();
            _unitOfWork.RelocationRepository.Update(request);
            _unitOfWork.RelocationRepository.Save();
        }
    }
}
