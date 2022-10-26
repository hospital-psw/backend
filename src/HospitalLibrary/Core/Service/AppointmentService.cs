namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository;
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

        public AppointmentService(ILogger<Appointment> logger) : base()
        {
            _logger = logger;
        }

        public override Appointment Get(int id)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.AppointmentRepository.Get(id);
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
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                unitOfWork.AppointmentRepository.Update(entity);
                unitOfWork.Save();

                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in AppointmentService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public Appointment GetAppointmentIfNotDone(int appointmentId)
        {
            try
            {
                using UnitOfWork unitOfWork = new UnitOfWork(new Settings.HospitalDbContext());
                return unitOfWork.AppointmentRepository.GetAppointmentIfNotDone(appointmentId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
