namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.DTO.Appointments;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
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

        public IEnumerable<RecommendedAppointmentDto> RecommendAppointments(RecommendRequestDto dto)
        {
            try
            {
                List<RecommendedAppointmentDto> generatedAppointments = GenerateAppointments(dto);
                List<Appointment> scheduledAppointments = _unitOfWork.AppointmentRepository.GetScheduledAppointments(dto.DoctorId, dto.PatientId).ToList();
                return RemoveScheduledAppointments(generatedAppointments, scheduledAppointments);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in Appointment service in GenerateFreeAppointments {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        private List<RecommendedAppointmentDto> GenerateAppointments(RecommendRequestDto dto)
        {
            try
            {
                List<RecommendedAppointmentDto> generatedAppointments = new List<RecommendedAppointmentDto>();
                ApplicationPatient patient = _unitOfWork.ApplicationPatientRepository.Get(dto.PatientId);
                ApplicationDoctor doctor = _unitOfWork.ApplicationDoctorRepository.Get(dto.DoctorId);
                Room room = _unitOfWork.RoomRepository.GetById(16);
                DateTime shiftIterator = doctor.WorkHours.Start;
                DateTime startDate = new DateTime(dto.Date.Year, dto.Date.Month, dto.Date.Day, doctor.WorkHours.Start.Hour, doctor.WorkHours.Start.Minute, doctor.WorkHours.Start.Second);

                while (shiftIterator < doctor.WorkHours.End)
                {
                    RecommendedAppointmentDto appointment = new RecommendedAppointmentDto(startDate, room.Number, room.Floor.Number, room.Floor.Building.Name); ;
                    generatedAppointments.Add(appointment);
                    shiftIterator = shiftIterator.AddMinutes(30);
                    startDate = startDate.AddMinutes(30);
                }

                return generatedAppointments;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in Appointment service in GenerateAppointments {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        private IEnumerable<RecommendedAppointmentDto> SelectExistingAppointmentsToRemove(List<Appointment> scheduledAppointments, RecommendedAppointmentDto generatedAppointment)
        {
            return from scheduledAppointment in scheduledAppointments where scheduledAppointment.Date.Equals(generatedAppointment.Date) select generatedAppointment;
        }

        private List<RecommendedAppointmentDto> GetAllAppointmentsThatShouldBeRemoved(List<RecommendedAppointmentDto> generatedAppointments, List<Appointment> scheduledAppointments)
        {
            List<RecommendedAppointmentDto> appointmentsToRemove = new List<RecommendedAppointmentDto>();
            generatedAppointments.ForEach(appointment => appointmentsToRemove.AddRange(SelectExistingAppointmentsToRemove(scheduledAppointments, appointment)));
            return appointmentsToRemove;
        }

        private List<RecommendedAppointmentDto> RemoveScheduledAppointments(List<RecommendedAppointmentDto> generatedAppointments, List<Appointment> scheduledAppointments)
        {
            GetAllAppointmentsThatShouldBeRemoved(generatedAppointments, scheduledAppointments).ForEach(appointment => generatedAppointments.Remove(appointment));

            return generatedAppointments;
        }

        public Appointment Create(NewAppointmentDto dto)
        {
            try
            {
                ApplicationPatient patient = _unitOfWork.ApplicationPatientRepository.Get(dto.PatientId);
                ApplicationDoctor doctor = _unitOfWork.ApplicationDoctorRepository.Get(dto.DoctorId);
                Room room = _unitOfWork.RoomRepository.GetById(16);
                Appointment newAppointment = new Appointment(dto.Date, dto.ExamType, null, patient, doctor);
                newAppointment.Room = room;
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
            catch (Exception e)
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
    }
}
