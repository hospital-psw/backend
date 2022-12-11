namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.DTO.MedicalTreatment;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.MedicalTreatment;
    using HospitalLibrary.Core.Model.Therapy;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Settings;
    using HospitalLibrary.Util;
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
                return _unitOfWork.MedicalTreatmentRepository.Get(id);
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
                _unitOfWork.MedicalTreatmentRepository.Update(entity);
                _unitOfWork.Save();

                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicalTreatmentService in Update {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override IEnumerable<MedicalTreatment> GetAll()
        {
            try
            {
                return _unitOfWork.MedicalTreatmentRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicalTreatmentService in GetAll {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public MedicalTreatment Add(NewMedicalTreatmentDto dto)
        {
            try
            {
                ApplicationPatient patient = _unitOfWork.ApplicationPatientRepository.Get(dto.PatientId);
                HospitalizePatietnt(patient);

                ApplicationDoctor doctor = _unitOfWork.ApplicationDoctorRepository.Get(dto.DoctorId);
                Room room = _unitOfWork.RoomRepository.GetById(dto.RoomId);

                AddPatientToRoom(room, patient);

                MedicalTreatment medicalTreatment = new MedicalTreatment(room, patient, doctor, new List<MedicamentTherapy>(), new List<BloodUnitTherapy>(), DateTime.Now, default(DateTime), true, "", dto.AdmittanceReason);

                SetupTreatment(medicalTreatment);

                _unitOfWork.Save();
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicalTreatmentService in Add {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        private void SetupTreatment(MedicalTreatment medicalTreatment)
        {
            _unitOfWork.MedicalTreatmentRepository.Add(medicalTreatment);
        }

        private void HospitalizePatietnt(ApplicationPatient patient)
        {
            patient.Hospitalized = true;
            _unitOfWork.ApplicationPatientRepository.Update(patient);
        }

        private void AddPatientToRoom(Room room, ApplicationPatient patient)
        {
            room.AddPatient(patient);
            _unitOfWork.RoomRepository.Update(room);
        }

        public void Delete(MedicalTreatment medicalTreatment)
        {
            try
            {
                medicalTreatment.Deleted = true;
                _unitOfWork.MedicalTreatmentRepository.Update(medicalTreatment);
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicalTreatmentService in Get {e.Message} in {e.StackTrace}");
            }
        }

        public MedicalTreatment ReleasePatient(MedicalTreatment medicalTreatment, string description)
        {
            try
            {
                SetTherapiesFinished(medicalTreatment);
                ReleasePatientFromRoom(medicalTreatment);
                SetPatientToNonHospitalized(medicalTreatment);
                SetTreatmentFinished(medicalTreatment, description);

                _unitOfWork.Save();
                return medicalTreatment;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicalTreatmentService in ReleasePatient {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        private void SetTherapiesFinished(MedicalTreatment treatment)
        {
            treatment.BloodUnitTherapies.ForEach(SetFinishTime);
            treatment.MedicamentTherapies.ForEach(SetFinishTime);
        }

        private void SetFinishTime(Therapy therapy)
        {
            therapy.End = therapy.End > DateTime.Now ? DateTime.Now : therapy.End;
        }

        private void SetTreatmentFinished(MedicalTreatment treatment, string description)
        {
            treatment.Report = description;
            treatment.Active = false;
            treatment.End = DateTime.Now;
            _unitOfWork.MedicalTreatmentRepository.Update(treatment);
        }

        private void ReleasePatientFromRoom(MedicalTreatment treatment)
        {
            treatment.Room.Patients.Remove(treatment.Patient);
            _unitOfWork.RoomRepository.Update(treatment.Room);
        }

        public void SetPatientToNonHospitalized(MedicalTreatment medicalTreatment)
        {
            medicalTreatment.Patient.Hospitalized = false;
            _unitOfWork.ApplicationPatientRepository.Update(medicalTreatment.Patient);
        }

        public IEnumerable<MedicalTreatment> GetActive()
        {
            try
            {
                return _unitOfWork.MedicalTreatmentRepository.GetActive();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicalTreatmentService in GetActive {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<MedicalTreatment> GetInactive()
        {
            try
            {
                return _unitOfWork.MedicalTreatmentRepository.GetInactive();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicalTreatmentService in GetInactive {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public void GeneratePdf(int id)
        {
            MedicalTreatment treatment = _unitOfWork.MedicalTreatmentRepository.Get(id);
            PDFUtil.GenerateTreatmentPdf(treatment);
        }
    }
}
