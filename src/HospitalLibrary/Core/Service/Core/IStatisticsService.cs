﻿namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IStatisticsService
    {
        public IEnumerable<int> GetNumberOfAppointmentsPerMonth();
        public (IEnumerable<string>, IEnumerable<int>) GetPatientsPerDoctor();
        public (List<int>, List<int>) GetNumberOfPatientsByAgeGroup();
        public List<int> GetUsersByType();

        public List<int> GetNumberOfVacationDaysPerMonth(int doctorId);
        List<int> GetNumberOfDoctorAppointmentsPerYear(int doctorId, int year);
        List<int> GetNumberOfDoctorAppointmentsPerMonth(int doctorId, int month, int year);
    }
}
