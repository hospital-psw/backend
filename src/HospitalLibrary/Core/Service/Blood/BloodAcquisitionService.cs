namespace HospitalLibrary.Core.Service.Blood
{
    using HospitalLibrary.Core.DTO.BloodManagment;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Blood.BloodManagment;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Repository;
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

        public BloodAcquisitionService(ILogger<BloodAcquisition> logger) : base()
        {
            _logger = logger;
        }


        public override IEnumerable<BloodAcquisition> GetAll()
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.BloodAcquisitionRepository.GetAll();
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
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.BloodAcquisitionRepository.Get(id);
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
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                Doctor doctor = unitOfWork.DoctorRepository.Get(acquisitionDTO.DoctorId);
                DateTime date = acquisitionDTO.Date;
                BloodType bloodType = acquisitionDTO.BloodType;
                int amount = acquisitionDTO.Amount;
                string reason = acquisitionDTO.Reason;
                BloodRequestStatus status = BloodRequestStatus.PENDING;
                BloodAcquisition bloodAcquisition = new BloodAcquisition(doctor, bloodType, amount, reason, date, status);
                unitOfWork.BloodAcquisitionRepository.Add(bloodAcquisition);
                unitOfWork.Save();

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
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                bloodAcquisition.Deleted = true;
                unitOfWork.BloodAcquisitionRepository.Update(bloodAcquisition);
                unitOfWork.Save();
            }
            catch (Exception)
            {

            }
        }

        public override BloodAcquisition Update(BloodAcquisition bloodAcquisition)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());

                unitOfWork.BloodAcquisitionRepository.Update(bloodAcquisition);
                unitOfWork.Save();
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
            using UnitOfWork unitOfWork = new(new HospitalDbContext());
            return unitOfWork.BloodAcquisitionRepository.GetPendingAcquisitions();
        }




    }
}
