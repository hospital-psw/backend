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
    }
}
