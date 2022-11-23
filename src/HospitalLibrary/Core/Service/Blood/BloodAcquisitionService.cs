namespace HospitalLibrary.Core.Service.Blood
{
    using HospitalLibrary.Core.DTO.BloodManagment;
    using HospitalLibrary.Core.Model;
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
                _logger.LogError($"Error in AppointmentService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override BloodAcquisition Get(int id)
        {
            try
            {
                
                return _unitOfWork.BloodAcquisitionRepository.Get(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in AppointmentService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public void Create(CreateAcquisitionDTO acquisitionDTO)
        {
            try
            {
                
                Doctor doctor = _unitOfWork.DoctorRepository.Get(acquisitionDTO.DoctorId);
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
                _logger.LogError($"Error in AppointmentService in Get {e.Message} in {e.StackTrace}");
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
            catch (Exception)
            {

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
                _logger.LogError($"Error in AppointmentService in Get {e.Message} in {e.StackTrace}");
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
            BloodAcquisition bloodAcquisition = _unitOfWork.BloodAcquisitionRepository.Get(id);
            bloodAcquisition.Status = BloodRequestStatus.ACCEPTED;
            BloodUnit bloodUnit = _unitOfWork.BloodUnitRepository.GetByBloodType(bloodAcquisition.BloodType);

            bloodUnit.Amount += bloodAcquisition.Amount;
            _unitOfWork.BloodUnitRepository.Update(bloodUnit);
            _unitOfWork.BloodAcquisitionRepository.Update(bloodAcquisition);
            _unitOfWork.Save();
            return bloodAcquisition;
        }

        public IEnumerable<BloodAcquisition> GetAcquisitionsForSpecificDoctor(int id) {
            return _unitOfWork.BloodAcquisitionRepository.GetAcquisitionsForSpecificDoctor(id);
         }



    }
}
