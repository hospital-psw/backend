namespace HospitalLibrary.Core.Service.Blood
{
    using HospitalLibrary.Core.DTO.BloodManagment;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Blood;
    using HospitalLibrary.Core.Model.Blood.BloodManagment;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Repository.Blood;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Blood.Core;
    using HospitalLibrary.Settings;
    using IdentityModel;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BloodAcquisitionService : BaseService<BloodAcquisition>, IBloodAcquisitionService
    {


        private readonly ILogger<BloodAcquisition> _logger;

        public BloodAcquisitionService(ILogger<BloodAcquisition> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _logger = logger;
        }


        public override IEnumerable<BloodAcquisition> GetAll()
        {
            try
            {
                return _unitOfWork.BloodAcquisitionRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodAcquisitionService in GetAll {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public void HandleBloodRequest(BloodRequestStatus status, int id, string managerComment)
        {
            BloodAcquisition bloodAcquisition = _unitOfWork.BloodAcquisitionRepository.Get(id);
            bloodAcquisition.Status = status;
            bloodAcquisition.ManagerComment = managerComment;
            _unitOfWork.BloodAcquisitionRepository.Update(bloodAcquisition);
            _unitOfWork.Save();
        }

        public override BloodAcquisition Get(int id)
        {
            try
            {
                return _unitOfWork.BloodAcquisitionRepository.Get(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodAcquisitionService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public void Create(CreateAcquisitionDTO acquisitionDTO)
        {
            try
            {
                ApplicationDoctor doctor = _unitOfWork.ApplicationDoctorRepository.Get(acquisitionDTO.DoctorId);
                DateTime date = acquisitionDTO.Date;
                BloodType bloodType = acquisitionDTO.BloodType;
                int amount = acquisitionDTO.Amount;
                string reason = acquisitionDTO.Reason;
                BloodRequestStatus status = BloodRequestStatus.PENDING;
                BloodAcquisition bloodAcquisition = new BloodAcquisition(doctor, bloodType, amount, reason, date, status);
                _unitOfWork.BloodAcquisitionRepository.Add(bloodAcquisition);
                _unitOfWork.Save();

            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodAcquisitionService in Create {e.Message} in {e.StackTrace}");
            }
        }

        public void Delete(BloodAcquisition bloodAcquisition)
        {
            try
            {
                bloodAcquisition.Deleted = true;
                _unitOfWork.BloodAcquisitionRepository.Update(bloodAcquisition);
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodAcquisitionService in Delete {e.Message} in {e.StackTrace}");
            }
        }

        public override BloodAcquisition Update(BloodAcquisition bloodAcquisition)
        {
            try
            {
                _unitOfWork.BloodAcquisitionRepository.Update(bloodAcquisition);
                _unitOfWork.Save();
                return bloodAcquisition;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodAcquisitionService in Get {e.Message} in {e.StackTrace}");
                return null;

            }
        }

        public IEnumerable<BloodAcquisition> GetPendingAcquisitions()
        {
            return _unitOfWork.BloodAcquisitionRepository.GetPendingAcquisitions();
        }


        public BloodAcquisition DeclineAcquisition(int id)
        {
            BloodAcquisition bloodAcquisition = _unitOfWork.BloodAcquisitionRepository.Get(id);
            bloodAcquisition.Status = BloodRequestStatus.DECLINED;
            _unitOfWork.BloodAcquisitionRepository.Update(bloodAcquisition);
            _unitOfWork.Save();
            return bloodAcquisition;
        }


        public BloodAcquisition AcceptAcquisition(int id)
        {
            try
            {
                BloodAcquisition bloodAcquisition = _unitOfWork.BloodAcquisitionRepository.Get(id);
                bloodAcquisition.Status = BloodRequestStatus.ACCEPTED;
                BloodUnit bloodUnit = _unitOfWork.BloodUnitRepository.GetByBloodType(bloodAcquisition.BloodType);
                bloodUnit.Amount += bloodAcquisition.Amount;
                _unitOfWork.BloodUnitRepository.Update(bloodUnit);
                _unitOfWork.BloodAcquisitionRepository.Update(bloodAcquisition);
                _unitOfWork.Save();
                return bloodAcquisition;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodAcquisitionService in AcceptedAcquisition {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<BloodAcquisition> GetAllAcceptedAcquisition()
        {
            try
            {
                return _unitOfWork.BloodAcquisitionRepository.GetAllAccepted();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodAcquisitionService in GetAllAcceptedAcquisition {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<BloodAcquisition> GetAllDeclinedAcquisition()
        {
            try
            {
                return _unitOfWork.BloodAcquisitionRepository.GetAllDeclined();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodAcquisitionService in GetAllDeclinedAcquisition {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<BloodAcquisition> GetAllPendingAcquisition()
        {
            try
            {
                return _unitOfWork.BloodAcquisitionRepository.GetAllPending();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodAcquisitionService in GetAllPendingAcquisition {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<BloodAcquisition> GetAllReconsideringAcquisition()
        {
            try
            {
                return _unitOfWork.BloodAcquisitionRepository.GetAllReconsidering();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodAcquisitionService in GetAllPendingAcquisition {e.Message} in {e.StackTrace}");
                return null;
            }


        }

        public IEnumerable<BloodAcquisition> GetAcquisitionsForSpecificDoctor(int id)
        {
            return _unitOfWork.BloodAcquisitionRepository.GetAcquisitionsForSpecificDoctor(id);
        }
    }
}
