namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAppointmentService
    {
        Appointment Get(int id);
        Appointment Update(Appointment appointment);
        IEnumerable<Appointment> GetAll();
    }
}