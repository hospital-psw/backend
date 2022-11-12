namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.DTO.MedicalTreatment;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.MedicalTreatment;
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

    public class MedicalTreatmentService : BaseService<MedicalTreatment>, IMedicalTreatmentService
    {

        private readonly ILogger<MedicalTreatment> _logger;

        public MedicalTreatmentService(ILogger<MedicalTreatment> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
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

        public override bool Delete(int id)
        {
            return base.Delete(id);
        }

        public override MedicalTreatment Update(MedicalTreatment entity)
        {
            return base.Update(entity);
        }

        public override IEnumerable<MedicalTreatment> GetAll()
        {
            return base.GetAll();
        }

        public MedicalTreatment Add(NewMedicalTreatmentDto dto)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                Patient patient = unitOfWork.PatientRepository.Get(dto.PatientId);
                Doctor doctor = unitOfWork.DoctorRepository.Get(dto.DoctorId);
                Room room = unitOfWork.RoomRepository.GetById(dto.RoomId);


                return new MedicalTreatment();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicalTreatmentService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public void Delete(MedicalTreatment medicalTreatment)
        {
            throw new NotImplementedException();
        }
    }
}
