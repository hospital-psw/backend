namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model.MedicalTreatment;
    using HospitalLibrary.Core.Model.Therapy;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Settings;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BloodUnitTherapyService : BaseService<BloodUnitTherapy>, IBloodUnitTherapyService
    {
        private readonly ILogger<BloodUnitTherapy> _logger;

        public BloodUnitTherapyService(ILogger<BloodUnitTherapy> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _logger = logger;
        }


        public BloodUnitTherapy Add(BloodUnitTherapy entity, int medicalTreatmentId)
        {
            try
            {
                //DODATI Umanjenje kolicine bloodunit-a kada ludi iki napravi

                MedicalTreatment medicalTreatment = _unitOfWork.MedicalTreatmentRepository.Get(medicalTreatmentId);
                medicalTreatment.BloodUnitTherapies.Add(entity);
                _unitOfWork.MedicalTreatmentRepository.Update(medicalTreatment);

                _unitOfWork.BloodUnitTherapyRepository.Add(entity);
                _unitOfWork.Save();

                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodUnitTherapyService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override BloodUnitTherapy Get(int id)
        {
            try
            {
                return _unitOfWork.BloodUnitTherapyRepository.Get(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodUnitTherapyService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override IEnumerable<BloodUnitTherapy> GetAll()
        {
            try
            {
                return _unitOfWork.BloodUnitTherapyRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodUnitTherapyService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override BloodUnitTherapy Update(BloodUnitTherapy entity)
        {
            try
            {
                _unitOfWork.BloodUnitTherapyRepository.Update(entity);
                _unitOfWork.Save();

                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodUnitTherapyService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public void Delete(BloodUnitTherapy bloodUnitTherapy)
        {
            try
            {
                bloodUnitTherapy.Deleted = true;
                _unitOfWork.BloodUnitTherapyRepository.Update(bloodUnitTherapy);
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodUnitTherapyService in Get {e.Message} in {e.StackTrace}");
            }
        }
    }
}
