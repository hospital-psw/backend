namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.DTO.MedicalTreatment;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.MedicalTreatment;
    using HospitalLibrary.Core.Model.Therapy;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Settings;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MedicalTreatmentService : BaseService<MedicalTreatment>, IMedicalTreatmentService
    {

        private readonly ILogger<MedicalTreatment> _logger;

        public MedicalTreatmentService(ILogger<MedicalTreatment> logger) : base()
        {
            _logger = logger;
        }

        public override MedicalTreatment Get(int id)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.MedicalTreatmentRepository.Get(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicalTreatmentService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override MedicalTreatment Update(MedicalTreatment entity)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                unitOfWork.MedicalTreatmentRepository.Update(entity);
                unitOfWork.Save();

                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicalTreatmentService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override IEnumerable<MedicalTreatment> GetAll()
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.MedicalTreatmentRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicalTreatmentService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public MedicalTreatment Add(NewMedicalTreatmentDto dto)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                Patient patient = unitOfWork.PatientRepository.Get(dto.PatientId);
                patient.Hospitalized = true;
                Doctor doctor = unitOfWork.DoctorRepository.Get(dto.DoctorId);
                Room room = unitOfWork.RoomRepository.GetById(dto.RoomId);

                MedicalTreatment medicalTreatment = new MedicalTreatment(room, patient, doctor, new List<MedicamentTherapy>(), new List<BloodUnitTherapy>(), DateTime.Now, default(DateTime), true, "");

                unitOfWork.MedicalTreatmentRepository.Add(medicalTreatment);
                unitOfWork.Save();
                
                return medicalTreatment;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicalTreatmentService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public void Delete(MedicalTreatment medicalTreatment)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                medicalTreatment.Deleted = true;
                unitOfWork.MedicalTreatmentRepository.Update(medicalTreatment);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicalTreatmentService in Get {e.Message} in {e.StackTrace}");
            }
        }
    }
}
