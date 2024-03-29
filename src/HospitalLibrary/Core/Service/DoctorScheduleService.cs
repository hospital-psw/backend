﻿namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.DTO.Appointments;
    using HospitalLibrary.Core.DTO.Consilium;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Domain;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.VacationRequests;
    using HospitalLibrary.Core.Model.ValueObjects;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Exceptions;
    using IdentityServer4.Extensions;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;
    using System.Threading.Tasks;

    public class DoctorScheduleService : BaseService<DoctorSchedule>, IDoctorScheduleService
    {
        private ILogger<DoctorSchedule> _logger;

        public DoctorScheduleService(ILogger<DoctorSchedule> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _logger = logger;
        }

        public override DoctorSchedule Get(int id)
        {
            try
            {
                return _unitOfWork.DoctorScheduleRepository.Get(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in DoctorScheduleService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override IEnumerable<DoctorSchedule> GetAll()
        {
            try
            {
                return _unitOfWork.DoctorScheduleRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in DoctorScheduleService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public Consilium TryToScheduleConsilium(ScheduleConsiliumDto dto)
        {
            if (dto.SelectedSpecializations.IsNullOrEmpty())
            {
                return TryToScheduleBySelectedDoctors(dto);
            }
            else if (dto.SelectedDoctors.IsNullOrEmpty())
            {
                return TryToScheduleBySelectedSpecializations(dto);
            }
            throw new ScheduleConsiliumException("Invalid data passed.");
        }

        private Consilium TryToScheduleBySelectedDoctors(ScheduleConsiliumDto dto)
        {
            List<ApplicationDoctor> selectedDoctors = _unitOfWork.ApplicationDoctorRepository.GetSelectedDoctors(dto.SelectedDoctors).ToList();
            return ScheduleBySelectedDoctors(dto, selectedDoctors);
        }

        private Consilium TryToScheduleBySelectedSpecializations(ScheduleConsiliumDto dto)
        {
            ApplicationDoctor doctor = _unitOfWork.ApplicationDoctorRepository.Get(dto.DoctorId);
            List<ApplicationDoctor> selectedSpecializationsDoctors = _unitOfWork.ApplicationDoctorRepository.GetDoctorsOfSelectedSpecializations(dto.SelectedSpecializations, doctor.WorkHours.Id).ToList();
            CheckIfNoAvailableDoctors(selectedSpecializationsDoctors);
            return ScheduleBySelectedSpecializations(dto, selectedSpecializationsDoctors);
        }

        private Consilium ScheduleBySelectedDoctors(ScheduleConsiliumDto dto, List<ApplicationDoctor> doctors)
        {
            ApplicationDoctor firstSelectedDoctor = doctors.First();

            for (DateTime date = dto.DateRange.From; date.CompareTo(dto.DateRange.To) <= 0; date = date.AddDays(1))
            {
                if (IsSomeDoctorUnavailable(doctors, date))
                {
                    continue;
                }
                foreach (DateTime appointment in GetDoctorAvailableAppointments(date, firstSelectedDoctor))
                {
                    if (CheckIfDoctorsAreAvailableInCurrentAppointment(doctors, appointment, date))
                    {
                        return CreateConsiliumEntity(date, appointment, doctors, dto);
                    }
                }
            }
            throw new ScheduleConsiliumException("One or more selected doctors are not able to attend the consilium in the given period.");
        }

        private List<DateTime> GetDoctorAvailableAppointments(DateTime date, ApplicationDoctor doctor)
        {
            RecommendRequestDto requestDto = new RecommendRequestDto(date, default(int), doctor.Id);
            List<DateTime> firstDoctorAvailableAppointments = GetDatesListOutOfDtoList(RecommendAppointments(requestDto).ToList());
            List<DateTime> firstDoctorConsiliumAppointments = GetDoctorsConsiliumsDateTimes(doctor.Id, date);
            return RemoveConsiliumFromAvailableAppointment(firstDoctorAvailableAppointments, firstDoctorConsiliumAppointments);
        }

        private List<DateTime> RemoveConsiliumFromAvailableAppointment(List<DateTime> availableAppointments, List<DateTime> consiliumsAppointments)
        {
            List<DateTime> available = new List<DateTime>();
            foreach (DateTime availableApp in availableAppointments)
            {
                if (!consiliumsAppointments.Exists(consiliumApp => availableApp.Hour == consiliumApp.Hour && availableApp.Minute == consiliumApp.Minute))
                {
                    available.Add(availableApp);
                }
            }
            return available;
        }

        private List<DateTime> GetDoctorsConsiliumsDateTimes(int doctorId, DateTime date)
        {
            List<DateTime> firstDoctorConsiliumAppointments = new List<DateTime>();
            _unitOfWork.ConsiliumRepository.GetDoctorsConsiliumsOfPassedDate(doctorId, date)
                .ToList()
                .ForEach(con => firstDoctorConsiliumAppointments.Add(con.DateTime));
            return firstDoctorConsiliumAppointments;
        }

        private void CheckIfNoAvailableDoctors(List<ApplicationDoctor> doctors)
        {
            if (doctors.IsNullOrEmpty())
            {
                throw new ScheduleConsiliumException("No available doctors.");
            }
        }

        private Consilium ScheduleBySelectedSpecializations(ScheduleConsiliumDto dto, List<ApplicationDoctor> doctors)
        {
            int maxPresence = default(int);
            DateTime dateOfMaxPresence = default(DateTime);
            DateTime timeOfMaxPresence = default(DateTime);
            List<ApplicationDoctor> presenceDoctors = new List<ApplicationDoctor>();

            for (DateTime date = dto.DateRange.From; date.CompareTo(dto.DateRange.To) <= 0; date = date.AddDays(1))
            {
                List<ApplicationDoctor> currentDayAvailableDoctors = CurrentDateAvailableDoctors(doctors, date);
                if (SpecializationsDontExistInDoctorList(dto.SelectedSpecializations, currentDayAvailableDoctors))
                {
                    continue;
                }
                List<DateTime> unionOfAllAvailableAppointments = UnionAllAvailableAppointments(currentDayAvailableDoctors, date);
                foreach (DateTime appointment in unionOfAllAvailableAppointments)
                {
                    int presence = 0;
                    List<ApplicationDoctor> currAvailableDoctors = DoctorsAvailableForCurrentAppointment(currentDayAvailableDoctors, date, appointment);
                    if (SpecializationsDontExistInDoctorList(dto.SelectedSpecializations, currAvailableDoctors))
                    {
                        continue;
                    }
                    presence = currAvailableDoctors.Count();
                    if (presence > maxPresence)
                    {
                        presenceDoctors = currAvailableDoctors;
                        maxPresence = presence;
                        timeOfMaxPresence = appointment;
                        dateOfMaxPresence = date;
                    }
                }
            }

            if (maxPresence < dto.SelectedSpecializations.Count())
            {
                throw new ScheduleConsiliumException("It is not possible to schedule consilium in the given period." +
                    " No doctor of any of the selected specializations is able to attend.");
            }
            else
            {
                return CreateConsiliumEntity(dateOfMaxPresence, timeOfMaxPresence, presenceDoctors, dto);
            }
        }

        private Consilium CreateConsiliumEntity(DateTime date, DateTime appointment, List<ApplicationDoctor> doctors, ScheduleConsiliumDto dto)
        {
            DateTime dateTime = new DateTime(date.Year, date.Month, date.Day, appointment.Hour, appointment.Minute, appointment.Second);
            List<DoctorSchedule> schedules = _unitOfWork.DoctorScheduleRepository.GetDoctorSchedulesByDoctorList(doctors).ToList();
            Room room = _unitOfWork.RoomRepository.GetById(dto.RoomId);
            Consilium consilium = new Consilium(dateTime, ConsiliumTopic.Enter(dto.Topic), dto.Duration, schedules, room);
            return consilium;
        }

        private bool CheckIfDoctorsAreAvailableInCurrentAppointment(List<ApplicationDoctor> doctors, DateTime currentAppointment, DateTime currentDate)
        {
            DateTime dt = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, currentAppointment.Hour, currentAppointment.Minute, currentAppointment.Second);
            return IsSomeDoctorUnavailable(doctors, dt);
        }

        private List<ApplicationDoctor> DoctorsAvailableForCurrentAppointment(List<ApplicationDoctor> currentDateAvailableDoctors, DateTime date, DateTime appointment)
        {
            DateTime dt = new DateTime(date.Year, date.Month, date.Day, appointment.Hour, appointment.Minute, appointment.Second);
            return CurrentDateAvailableDoctors(currentDateAvailableDoctors, dt);
        }

        private List<ApplicationDoctor> CurrentDateAvailableDoctors(List<ApplicationDoctor> doctors, DateTime date)
        {
            List<ApplicationDoctor> availableDoctors = new List<ApplicationDoctor>();
            List<DoctorSchedule> doctorSchedules = _unitOfWork.DoctorScheduleRepository.GetDoctorSchedulesByDoctorList(doctors).ToList();
            foreach (DoctorSchedule doctorSchedule in doctorSchedules)
            {
                if (doctorSchedule.IsDoctorAvailable(date))
                {
                    availableDoctors.Add(doctorSchedule.Doctor);
                }
            }
            return availableDoctors;
        }

        private List<DateTime> UnionAllAvailableAppointments(List<ApplicationDoctor> availableDoctors, DateTime date)
        {
            List<DateTime> allAvailableAppointments = new List<DateTime>();
            foreach (ApplicationDoctor doc in availableDoctors)
            {
                RecommendRequestDto dto = new RecommendRequestDto(date, default(int), doc.Id);
                List<DateTime> dates = GetDoctorAvailableAppointments(date, doc);
                allAvailableAppointments.AddRange(dates);
            }
            allAvailableAppointments = allAvailableAppointments.GroupBy(x => new { x.Hour, x.Minute }).Select(y => y.First()).ToList();
            allAvailableAppointments = allAvailableAppointments.OrderBy(x => x.TimeOfDay).ToList();
            return allAvailableAppointments;
        }

        private bool SpecializationsDontExistInDoctorList(List<Specialization> selectedSpecializations, List<ApplicationDoctor> doctors)
        {
            if (selectedSpecializations.Any(spec => !doctors.Exists(x => x.Specialization == spec)))
            {
                return true;
            }
            return false;
        }

        private bool IsSomeDoctorUnavailable(List<ApplicationDoctor> doctors, DateTime date)
        {
            List<DoctorSchedule> doctorSchedules = _unitOfWork.DoctorScheduleRepository.GetDoctorSchedulesByDoctorList(doctors).ToList();
            foreach (DoctorSchedule ds in doctorSchedules)
            {
                if (!ds.IsDoctorAvailable(date))
                {
                    return true;
                }
            }
            return false;
        }

        public List<DateTime> GetDatesListOutOfDtoList(List<RecommendedAppointmentDto> dtoList)
        {
            List<DateTime> dates = new List<DateTime>();
            dtoList.ForEach(dto => dates.Add(dto.Date));
            return dates;
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
                Room room = _unitOfWork.RoomRepository.GetById(doctor.Office.Id);
                DateTime shiftIterator = doctor.WorkHours.Start;
                DateTime startDate = new DateTime(dto.Date.Year, dto.Date.Month, dto.Date.Day, doctor.WorkHours.Start.Hour, doctor.WorkHours.Start.Minute, doctor.WorkHours.Start.Second);

                while (shiftIterator < doctor.WorkHours.End)
                {
                    RecommendedAppointmentDto appointment = new RecommendedAppointmentDto(startDate, room.Number, room.Floor.Number.Number, room.Floor.Building.Name); ;
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

        public DoctorSchedule GetDoctorsSchedule(int id)
        {
            return _unitOfWork.DoctorScheduleRepository.GetDoctorScheduleByDoctorId(id);
        }
    }
}
