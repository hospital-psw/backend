﻿namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.DTO.Appointments;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Domain;
    using HospitalLibrary.Core.Model.Enums;
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

        public void Delete(Appointment appointment)
        {
            try
            {
                appointment.Deleted = true;
                _unitOfWork.AppointmentRepository.Update(appointment);
                _unitOfWork.Save();
            }
            catch (Exception)
            {
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

        public IEnumerable<Appointment> GetAllBySpecialization(Specialization specialization, DateRange dateRange)
        {
            try
            {
                List<Appointment> appointments = _unitOfWork.AppointmentRepository.GetAllBySpecialization(specialization, dateRange);
                return appointments;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        private IEnumerable<ReccomendedBySpecializationDTO> GenerateBySpecialization(Specialization specialization, ReccomendBySpecializationRequestDto dto, DateRange dateRange)
        {
            try
            {
                List<ReccomendedBySpecializationDTO> generatedAppointments = new List<ReccomendedBySpecializationDTO>();
                ApplicationPatient patient = _unitOfWork.ApplicationPatientRepository.Get(dto.PatientId);
                List<ApplicationDoctor> doctors = (List<ApplicationDoctor>)_unitOfWork.ApplicationDoctorRepository.GetBySpecialization(specialization);
                Room room = _unitOfWork.RoomRepository.GetById(3);

                foreach (ApplicationDoctor doctor in doctors)
                {
                    DateTime shiftIterator = doctor.WorkHours.Start;
                    DateTime dayIterator = new DateTime(dateRange.From.Year, dateRange.From.Month, dateRange.From.Day);

                    while (dayIterator <= dateRange.To)
                    {
                        DateTime startDate = new DateTime(dayIterator.Year, dayIterator.Month, dayIterator.Day, doctor.WorkHours.Start.Hour, doctor.WorkHours.Start.Minute, doctor.WorkHours.Start.Second);
                        while (shiftIterator <= doctor.WorkHours.End)
                        {
                            ReccomendedBySpecializationDTO appointment = new ReccomendedBySpecializationDTO(doctor.Id, doctor.FirstName, doctor.LastName, dayIterator, 30, room.Number, room.Floor.Number.Number, room.Floor.Building.Name);
                            generatedAppointments.Add(appointment);
                            shiftIterator =shiftIterator.AddMinutes(30);
                            startDate.AddMinutes(30);
                        }

                        dayIterator.AddDays(1);
                    }
                }

                return generatedAppointments;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        private IEnumerable<ReccomendedBySpecializationDTO> SelectExistingBySpecializationToRemove(List<Appointment> scheduledAppointments, ReccomendedBySpecializationDTO generatedAppointment)
        {
            return from scheduledAppointment in scheduledAppointments where scheduledAppointment.Date.Equals(generatedAppointment.Date) select generatedAppointment;
        }

        private List<ReccomendedBySpecializationDTO> GetAllRemovableAppointmentsBySecialization(List<ReccomendedBySpecializationDTO> generatedAppointments, List<Appointment> scheduledAppointments)
        {
            List<ReccomendedBySpecializationDTO> appointmentsToRemove = new List<ReccomendedBySpecializationDTO>();
            generatedAppointments.ForEach(appointment => appointmentsToRemove.AddRange(SelectExistingBySpecializationToRemove(scheduledAppointments, appointment)));
            return appointmentsToRemove;
        }

        private List<ReccomendedBySpecializationDTO> RemoveScheduledAppointmentsBySpecialization(List<ReccomendedBySpecializationDTO> generatedAppointments, List<Appointment> scheduledAppointments)
        {
            GetAllRemovableAppointmentsBySecialization(generatedAppointments, scheduledAppointments).ForEach(appointment => generatedAppointments.Remove(appointment));

            return generatedAppointments;
        }

        public IEnumerable<ReccomendedBySpecializationDTO> RecommendAppointmentsBySpecialization(ReccomendBySpecializationRequestDto dto, Specialization specialization)
        {
            try
            {
                DateRange dateRange = new DateRange(dto.DateRange.From, dto.DateRange.To);
                List<ReccomendedBySpecializationDTO> generatedAppointments = GenerateBySpecialization(specialization, dto, dateRange).ToList();
                List<Appointment> scheduledAppointments = _unitOfWork.AppointmentRepository.GetAllBySpecialization(specialization, dateRange).ToList();
                return RemoveScheduledAppointmentsBySpecialization(generatedAppointments, scheduledAppointments);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
