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
        private readonly ILogger<RelocationRequest> _logger;

        public RelocationService(ILogger<RelocationRequest> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _logger = logger;
        }

        public RelocationService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public List<DateTime> GetAvailableAppointmentsForRoom(int roomId, DateTime from, DateTime to, int duration)
        {
            try
            {
                List<DateTime> dateTimes = new List<DateTime>();
                List<Appointment> scheduledAppointments = _unitOfWork.AppointmentRepository.GetScheduledAppointmentsForRoom(roomId, from, to).ToList();

                DateTime startTime = from;
                while (startTime < to)
                {
                    bool catchedAppointment = false;
                    DateTime endTime = startTime.AddHours(duration);
                    foreach (Appointment appointment in scheduledAppointments)
                    {
                        if (appointment.Date >= startTime && appointment.Date.AddMinutes(30) <= endTime)
                        {
                            catchedAppointment = true;
                            startTime = appointment.Date.AddMinutes(30);
                            endTime = startTime.AddHours(duration);
                            break;
                        }
                    }
                    
                    if (!catchedAppointment)
                    {
                        dateTimes.Add(startTime);
                        startTime = endTime;
                    }
                }

                return dateTimes;
            }
            catch (Exception e)
            {
                return null;
            }
        }


     
    }
}
