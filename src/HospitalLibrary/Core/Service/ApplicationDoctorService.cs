namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ApplicationDoctorService : BaseService<ApplicationDoctor>, IApplicationDoctorService
    {
        private readonly ILogger<ApplicationDoctor> _logger;
        public ApplicationDoctorService(ILogger<ApplicationDoctor> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _logger = logger;
        }

        public IEnumerable<ApplicationDoctor> RecommendDoctors()
        {
            List<ApplicationDoctor> result = new List<ApplicationDoctor>();
            List<int> numberOfPatients = new List<int>();
            int min;
            foreach (ApplicationDoctor i in _unitOfWork.ApplicationUserRepository.GetAllGeneralDoctors())
            {
                numberOfPatients.Add(GetNumberOfPatientsForDoctor(i));
            }
            min = numberOfPatients.Min();

            foreach (ApplicationDoctor i in _unitOfWork.ApplicationUserRepository.GetAllGeneralDoctors())
            {
                if (GetNumberOfPatientsForDoctor(i) <= min + 2)
                {
                    result.Add(i);
                }
            }
            return result;
        }

        public int GetNumberOfPatientsForDoctor(ApplicationDoctor appDoctor)
        {
            List<ApplicationPatient> doctorsPatients = new List<ApplicationPatient>();
            foreach (ApplicationPatient i in _unitOfWork.ApplicationUserRepository.GetAllPatients())
            {
                if (i.applicationDoctor == appDoctor)
                {
                    doctorsPatients.Add(i);
                }
            }
            return doctorsPatients.Count();
        }
    }


}
