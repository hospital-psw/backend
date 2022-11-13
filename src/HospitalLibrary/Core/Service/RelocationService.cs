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

        public List<DateTime> GetAvailableAppointments(int roomId1, int roomId2, DateTime from, DateTime to, int duration)
        {
            try
            {
                List<DateTime> dateTimes = new List<DateTime>();
                DateTime startTime = from;

                while (startTime < to)
                {
                    DateTime endTime = startTime.AddHours(duration);
                    DateTime? newStartTime = IsRoomAvailable(roomId1, startTime, endTime);
                    if(newStartTime != null)
                    {
                        startTime = (DateTime)newStartTime;
                        endTime = startTime.AddHours(duration);
                    }
                    else
                    {
                        newStartTime = IsRoomAvailable(roomId2, startTime, endTime);
                        if (newStartTime != null)
                        {
                            startTime = (DateTime)newStartTime;
                        }
                        else
                        {
                            dateTimes.Add(startTime);
                            startTime = endTime;
                        }
                    }
                }

                return dateTimes;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DateTime? IsRoomAvailable(int roomId, DateTime startTime, DateTime endTime)
        {
            try
            {
                List<Appointment> scheduledAppointments = _unitOfWork.AppointmentRepository.GetScheduledAppointmentsForRoom(roomId).ToList();
                foreach (Appointment appointment in scheduledAppointments)
                {
                    if (appointment.Date >= startTime && appointment.Date.AddMinutes(30) <= endTime) return appointment.Date.AddMinutes(30);
                }

                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

     
    }
}
