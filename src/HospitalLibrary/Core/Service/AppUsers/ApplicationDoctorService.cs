namespace HospitalLibrary.Core.Service.AppUsers
{
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.AppUsers.Core;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ApplicationDoctorService : BaseService<ApplicationDoctor>, IApplicationDoctorService
    {
        private ILogger<ApplicationDoctor> _logger;

        public ApplicationDoctorService(ILogger<ApplicationDoctor> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _logger = logger;
        }

        public override ApplicationDoctor Get(int id)
        {
            try
            {
                return _unitOfWork.ApplicationDoctorRepository.Get(id);
            }
            catch (Exception e)
            {

                _logger.LogError($"Error in ApplicationDoctorService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override IEnumerable<ApplicationDoctor> GetAll()
        {
            try
            {
                return _unitOfWork.ApplicationDoctorRepository.GetAll();
            }
            catch (Exception e)
            {

                _logger.LogError($"Error in ApplicationDoctorService in GetAll {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<ApplicationDoctor> GetBySpecialization(Specialization specialization)
        {
            try
            {
                return _unitOfWork.ApplicationDoctorRepository.GetBySpecialization(specialization);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ApplicationDoctorService in GetBySpecialization {e.Message} in {e.StackTrace}");
                return null;
            }
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

        private int GetNumberOfPatientsForDoctor(ApplicationDoctor appDoctor)
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
