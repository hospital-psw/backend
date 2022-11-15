﻿namespace HospitalLibrary.Core.Repository.Core
{
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {

        public IEnumerable<Appointment> GetAppointmentsForPatient(int patientId);
        public IEnumerable<Appointment> GetAppointmentsForDoctor(int doctorId);
        public IEnumerable<Appointment> GetScheduledAppointments(int doctorId, int patientId);

        public IEnumerable<Appointment> GetAppointmentsInDateRangeDoctor(int doctorId, DateTime from, DateTime to);
    }
}
