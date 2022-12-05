namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RoomScheduleService : IRoomScheduleService
    {

        protected readonly IUnitOfWork _unitOfWork;
        public RoomScheduleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public List<DateTime> GetAppointments(List<int> rooms, DateTime from, DateTime to, int duration)
        {
            DateTime startTime = new DateTime(from.Year, from.Month, from.Day, 0, 0, 0);
            DateTime toTime = new DateTime(to.Year, to.Month, to.Day + 1, 0, 0, 0);
            DateTime currentDateTime = DateTime.Now;
            if (startTime.Day == currentDateTime.Day) startTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, currentDateTime.Hour + 1, 0, 0);
            return GetAvailableAppointments(rooms, startTime, toTime, duration);
        }

        public List<DateTime> GetAvailableAppointments(List<int> rooms, DateTime startTime, DateTime toTime, int duration)
        {
            try
            {
                List<DateTime> dateTimes = new List<DateTime>();
                while (startTime.AddHours(duration) <= toTime)
                {
                    DateTime newStartTime = startTime;
                    foreach (int room in rooms)
                    {
                        newStartTime = CheckRoom(room, newStartTime, newStartTime.AddHours(duration));
                    }
                    if (newStartTime == startTime)
                    {
                        dateTimes.Add(startTime);
                        startTime = startTime.AddHours(duration);
                    }
                    else
                    {
                        startTime = newStartTime;
                    }
                }

                return dateTimes;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DateTime CheckRoom(int roomId, DateTime startTime, DateTime endTime)
        {
            DateTime? newStartTime = IsRoomAvailable(roomId, startTime, endTime);
            if (newStartTime != null) startTime = (DateTime)newStartTime;
            return startTime;
        }

        public DateTime? IsRoomAvailable(int roomId, DateTime startTime, DateTime endTime)
        {
            try
            {
                if (CheckAppointments(roomId, startTime, endTime) != null) return CheckAppointments(roomId, startTime, endTime);
                else if (CheckRelocationRequests(roomId, startTime, endTime) != null) return CheckRelocationRequests(roomId, startTime, endTime);
                else return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private DateTime? CheckAppointments(int roomId, DateTime startTime, DateTime endTime)
        {
            foreach (Appointment appointment in _unitOfWork.AppointmentRepository.GetScheduledAppointmentsForRoom(roomId).ToList())
            {
                if (StartsBeforeEndsDuringScheduled(startTime, endTime, appointment.Date) || StartsAndEndsDuringScheduled(startTime, endTime, appointment.Date, appointment.Date.AddMinutes(30)) || StartsDuringAndEndsAfterScheduled(startTime, endTime, appointment.Date.AddMinutes(30))) return appointment.Date.AddMinutes(30);
            }
            return null;
        }


        private DateTime? CheckRelocationRequests(int roomId, DateTime startTime, DateTime endTime)
        {
            foreach (RelocationRequest request in _unitOfWork.RelocationRepository.GetScheduledRelocationsForRoom(roomId).ToList())
            {
                if (StartsBeforeEndsDuringScheduled(startTime, endTime, request.StartTime) || StartsAndEndsDuringScheduled(startTime, endTime, request.StartTime, request.StartTime.AddHours(request.Duration)) || StartsDuringAndEndsAfterScheduled(startTime, endTime, request.StartTime.AddHours(request.Duration))) return request.StartTime.AddHours(request.Duration);
            }
            return null;
        }

        private bool StartsBeforeEndsDuringScheduled(DateTime startTime, DateTime endTime, DateTime scheduledStartTime)
        {
            return startTime <= scheduledStartTime && endTime > scheduledStartTime;
        }

        private bool StartsAndEndsDuringScheduled(DateTime startTime, DateTime endTime, DateTime scheduledStartTime, DateTime scheduledEndTime)
        {
            return startTime >= scheduledStartTime && endTime <= scheduledEndTime;
        }

        private bool StartsDuringAndEndsAfterScheduled(DateTime startTime, DateTime endTime, DateTime scheduledEndTime)
        {
            return startTime < scheduledEndTime && endTime >= scheduledEndTime;
        }

    }
}
