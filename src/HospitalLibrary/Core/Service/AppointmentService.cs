namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.DTO.Appointments;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.ValueObjects;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using IdentityServer4.Extensions;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class AppointmentService : BaseService<Appointment>, IAppointmentService
    {
        private readonly ILogger<Appointment> _logger;

        public AppointmentService(ILogger<Appointment> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _logger = logger;
        }

        public override Appointment Get(int id)
        {
            try
            {
                return _unitOfWork.AppointmentRepository.Get(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in AppointmentService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override Appointment Update(Appointment entity)
        {
            try
            {
                _unitOfWork.AppointmentRepository.Update(entity);
                _unitOfWork.Save();

                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in AppointmentService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public Appointment Create(NewAppointmentDto dto)
        {
            try
            {
                ApplicationPatient patient = _unitOfWork.ApplicationPatientRepository.Get(dto.PatientId);
                ApplicationDoctor doctor = _unitOfWork.ApplicationDoctorRepository.Get(dto.DoctorId);
                Room room = _unitOfWork.RoomRepository.GetById(26);
                Appointment newAppointment = new Appointment(dto.Date, dto.ExamType, null, patient, doctor);
                newAppointment.Room = room;
                DoctorSchedule doctorSchedule = _unitOfWork.DoctorScheduleRepository.GetDoctorScheduleByDoctorId(dto.DoctorId);
                doctorSchedule.Appointments.Add(newAppointment);
                _unitOfWork.DoctorScheduleRepository.Update(doctorSchedule);
                _unitOfWork.AppointmentRepository.Add(newAppointment);
                _unitOfWork.Save();
                return newAppointment;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in Appointment service in Create appointment {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public void Delete(Appointment appointment, CancellationInfo info)
        {
            try
            {
                appointment.Deleted = true;
                appointment.CancellationInfo = info;
                _unitOfWork.AppointmentRepository.Update(appointment);
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in Appointment service in Delete {e.Message} in {e.StackTrace}");
                throw;
            }
        }

        public void CancelAppointment(int id)
        {
            try
            {
                Appointment appointment = _unitOfWork.AppointmentRepository.Get(id);
                appointment.Deleted = true;
                _unitOfWork.AppointmentRepository.Update(appointment);
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in Appointment service in Delete {e.Message} in {e.StackTrace}");
                throw;
            }
        }

        public IEnumerable<Appointment> GetByDoctorsId(int doctorId)
        {
            try
            {
                List<Appointment> appointments = (List<Appointment>)_unitOfWork.AppointmentRepository.GetAppointmentsForDoctor(doctorId);

                return appointments;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Appointment> GetByPatientsId(int patientId)
        {
            try
            {
                return _unitOfWork.AppointmentRepository.GetAppointmentsForPatient(patientId);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in Appointment service in GetByPatientsId appointment {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<Appointment> GetAppointmentsInDateRangeDoctor(int doctorId, DateTime from, DateTime to)
        {
            try
            {
                List<Appointment> appointments = (List<Appointment>)_unitOfWork.AppointmentRepository.GetAppointmentsInDateRangeDoctor(doctorId, from, to);
                return appointments;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Appointment> GetAllForRoom(int roomId)
        {
            List<Appointment> appointments = _unitOfWork.AppointmentRepository.GetAllForRoom(roomId);
            List<Appointment> futureAppointments = new List<Appointment>();
            foreach (Appointment appointment in appointments)
            {
                if (appointment.Date >= DateTime.Now) futureAppointments.Add(appointment);
            }
            return futureAppointments;
        }

        public IEnumerable<Appointment> GetAppointmentsForDoctor(int id)
        {
            try
            {
                IEnumerable<Appointment> appointments = _unitOfWork.AppointmentRepository.GetAppointmentsForDoctor(id);
                return appointments;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<ApplicationDoctor> GetAvailableDoctorsForEmergencyAppointment(DateTime emergencyStartDate) {
            
            List<ApplicationDoctor> availableDoctors = new List<ApplicationDoctor>();

            foreach(ApplicationDoctor doctor in _unitOfWork.ApplicationDoctorRepository.GetAll())
            {
                if(GetAppointmentsInDateRangeDoctor(doctor.Id,emergencyStartDate,emergencyStartDate.AddMinutes(30)).IsNullOrEmpty())
                {
                    availableDoctors.Add(doctor);
                }
            }
            return availableDoctors;
        }
    }
}
