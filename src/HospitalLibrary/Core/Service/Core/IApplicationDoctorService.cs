namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.Model.ApplicationUser;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IApplicationDoctorService
    {
        IEnumerable<ApplicationDoctor> RecommendDoctors();
        int GetNumberOfPatientsForDoctor(ApplicationDoctor appDoctor);
    }
}
