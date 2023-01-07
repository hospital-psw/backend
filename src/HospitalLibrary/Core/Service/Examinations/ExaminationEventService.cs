namespace HospitalLibrary.Core.Service.Examinations
{
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Events;
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Model.Medicament;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Examinations.Core;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ExaminationEventService : BaseService<DomainEvent>, IExaminationEventService
    {
        private readonly ILogger<ExaminationEventService> _logger;

        public ExaminationEventService(IUnitOfWork unitOfWork, ILogger<ExaminationEventService> logger) : base(unitOfWork)
        {
            _logger = logger;
        }

        public Anamnesis StartExamination(ExaminationStarted examinationStarted)
        {
            try
            {
                Anamnesis anamnesis = _unitOfWork.AnamnesisRepository.GetByAppointment(examinationStarted.AppointmentId);
                if (anamnesis.Appointment.IsDone) return anamnesis;
                if (anamnesis == null)
                {
                    Appointment appointment = _unitOfWork.AppointmentRepository.Get(examinationStarted.AppointmentId);
                    anamnesis = Anamnesis.Create(appointment);
                    _unitOfWork.AnamnesisRepository.Add(anamnesis);
                    _unitOfWork.Save();
                }

                anamnesis.StartExamination(new ExaminationStarted(anamnesis.Id, examinationStarted.TimeStamp, examinationStarted.EventName, examinationStarted.AppointmentId));
                _unitOfWork.Save();
                return anamnesis;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ExaminationEventService in StartExamination {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public Anamnesis ManageSymptoms(SymptomsChanged symptomsChanged)
        {
            try
            {
                Anamnesis anamnesis = _unitOfWork.AnamnesisRepository.GetUnfinishedAnamnesis(symptomsChanged.AggregateId);
                if (anamnesis == null) return null;

                Symptom symptom = _unitOfWork.SymptomRepository.Get(symptomsChanged.SymptomId);
                anamnesis.ChangeSymptoms(symptomsChanged, symptom);
                _unitOfWork.Save();
                return anamnesis;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ExaminationEventService in ManageSymptoms {e.Message} in {e.StackTrace}");
                return null;
            }

        }

        public Anamnesis CreateDescription(DescriptionCreated descriptionCreated)
        {
            try
            {
                Anamnesis anamnesis = _unitOfWork.AnamnesisRepository.GetUnfinishedAnamnesis(descriptionCreated.AggregateId);
                if (anamnesis == null) return null;

                anamnesis.AddDescription(descriptionCreated);
                _unitOfWork.Save();
                return anamnesis;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ExaminationEventService in Create Description {e.Message} in {e.StackTrace}");
                return null;
            }

        }

        public Anamnesis CreatePrescription(PrescriptionCreated prescriptionCreated)
        {
            try
            {
                Anamnesis anamnesis = _unitOfWork.AnamnesisRepository.GetUnfinishedAnamnesis(prescriptionCreated.AggregateId);
                if (anamnesis == null) return null;

                Medicament medicament = _unitOfWork.MedicamentRepository.Get(prescriptionCreated.MedicamentId);
                Prescription prescription = new Prescription(medicament, prescriptionCreated.Description, new Model.Domain.DateRange(prescriptionCreated.From, prescriptionCreated.To));

                anamnesis.AddPrescription(prescriptionCreated, prescription);
                _unitOfWork.Save();
                return anamnesis;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ExaminationEventService in CreatePrescription {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public Anamnesis RemovePrescription(PrescriptionRemoved prescriptionRemoved)
        {
            try
            {
                Anamnesis anamnesis = _unitOfWork.AnamnesisRepository.GetUnfinishedAnamnesis(prescriptionRemoved.AggregateId);
                if (anamnesis == null) return null;

                anamnesis.RemovePrescription(prescriptionRemoved);
                _unitOfWork.Save();
                return anamnesis;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ExaminationEventService in RemovePrescription {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public Anamnesis FinishExamination(ExaminationFinished examinationFinished)
        {
            try
            {
                Anamnesis anamnesis = _unitOfWork.AnamnesisRepository.GetUnfinishedAnamnesis(examinationFinished.AggregateId);
                if (anamnesis == null) return null;

                if (string.IsNullOrEmpty(anamnesis.Description)) return anamnesis;

                anamnesis.FinishExamination(examinationFinished);
                _unitOfWork.Save();
                return anamnesis;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ExaminationEventService in FinishExamination {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public Anamnesis ExecuteEvent(ExaminationEvent examinationEvent)
        {
            try
            {
                Anamnesis anamnesis = _unitOfWork.AnamnesisRepository.GetUnfinishedAnamnesis(examinationEvent.AggregateId);
                if (anamnesis == null) return null;

                anamnesis.ExecuteEvent(examinationEvent);
                _unitOfWork.Save();
                return anamnesis;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ExaminationEventService in ExecuteEvent {e.Message} in {e.StackTrace}");
                return null;
            }
        }

    }
}
