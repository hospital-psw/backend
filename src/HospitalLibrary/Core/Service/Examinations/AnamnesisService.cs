namespace HospitalLibrary.Core.Service.Examinations
{
    using HospitalLibrary.Core.DTO.Examinations;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Domain;
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Examinations.Core;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AnamnesisService : BaseService<Anamnesis>, IAnamnesisService
    {

        private readonly ILogger<AnamnesisService> _logger;

        public AnamnesisService(IUnitOfWork unitOfWork, ILogger<AnamnesisService> logger) : base(unitOfWork)
        {
            _logger = logger;
        }

        public override IEnumerable<Anamnesis> GetAll()
        {
            try
            {
                return _unitOfWork.AnamnesisRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in GetAll in AnamnesisService {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override Anamnesis Get(int id)
        {
            try
            {
                return _unitOfWork.AnamnesisRepository.Get(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in Get in AnamnesisService {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<Anamnesis> GetByDoctor(int doctorId)
        {
            try
            {
                return _unitOfWork.AnamnesisRepository.GetByDoctor(doctorId);
            }
            catch(Exception e)
            {
                _logger.LogError($"Error in GetByDoctor in AnamnesisService {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<Anamnesis> GetByPatient(int patientId)
        {
            try
            {
                return _unitOfWork.AnamnesisRepository.GetByPatient(patientId);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in GetByPatient in AnamnesisService {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<Anamnesis> GetInDateRange(DateRange dateRange)
        {
            try
            {
                return _unitOfWork.AnamnesisRepository.GetInDateRange(dateRange);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in GetInDateRange in AnamnesisService {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public Anamnesis Add(NewAnamnesisDto dto)
        {

            Appointment appointment = _unitOfWork.AppointmentRepository.Get(dto.AppointmentId);

            if (appointment == null) throw new Exception("Appointment doesn't exist");

            List<Symptom> symptoms = new List<Symptom>();

            if (dto.SymptomIds != null)
                symptoms = _unitOfWork.SymptomRepository.GetSelectedSymptoms(dto.SymptomIds).ToList();

            Anamnesis newAnamnesis = new Anamnesis(appointment, dto.Description);
            newAnamnesis.Symptoms = symptoms;

            appointment.IsDone = true;
            _unitOfWork.AppointmentRepository.Update(appointment);
            _unitOfWork.AnamnesisRepository.Add(newAnamnesis);
            _unitOfWork.Save();

            return newAnamnesis;
        }

        public Anamnesis AddPrescriptions(int anamnesisId, List<Prescription> prescriptions)
        {
            Anamnesis anamnesis = _unitOfWork.AnamnesisRepository.Get(anamnesisId);
            anamnesis.Prescriptions = prescriptions;
            _unitOfWork.AnamnesisRepository.Update(anamnesis);
            //prescriptions.ForEach(_unitOfWork.PrescriptionRepository.Update);
            _unitOfWork.Save();
            return anamnesis;
        }
    }
}
