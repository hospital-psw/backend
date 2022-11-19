namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IStatisticsService
    {
        public IEnumerable<int> GetNumberOfAppointmentsPerMonth();
        public IEnumerable<string> getDoctorNames();

        public int getNumberOfDoctorsPatients();
    }
}
