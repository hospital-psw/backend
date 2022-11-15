namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.DTO.Appointments;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RelocationService : BaseService<RelocationRequest>, IRelocationService
    {

        public RelocationService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public RelocationRequest Create(RelocationRequest entity)
        {
            try
            {
                return _unitOfWork.RelocationRepository.Create(entity);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<DateTime> GetAvailableAppointments(int roomId1, int roomId2, DateTime startTime, DateTime to, int duration)
        {
            try
            {
                List<DateTime> dateTimes = new List<DateTime>();
                while (startTime.AddHours(duration) <= to)
                {
                    DateTime endTime = startTime.AddHours(duration);
                    DateTime newStartTime = CheckRoom(roomId1, startTime, endTime);
                    endTime = newStartTime.AddHours(duration);
                    newStartTime = CheckRoom(roomId2, newStartTime, endTime);
                    endTime = startTime.AddHours(duration);
                    if (newStartTime == startTime)
                    {
                        dateTimes.Add(startTime);
                        startTime = endTime;
                    }
                    else
                    {
                        startTime = newStartTime;
                    }

                }

                return dateTimes;
            }
            catch (Exception e)
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
            catch (Exception e)
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
