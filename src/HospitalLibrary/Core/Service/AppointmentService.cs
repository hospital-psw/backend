namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Service.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AppointmentService : BaseService<Appointment>, IAppointmentService
    {
        public AppointmentService() : base() { }

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