namespace HospitalLibrary.Core.Service.AppUsers
{
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.AppUsers.Core;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ApplicationPatientService : BaseService<ApplicationPatient>, IApplicationPatientService
    {
        private readonly ILogger<ApplicationPatient> _logger;

        public ApplicationPatientService(ILogger<ApplicationPatient> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _logger = logger;
        }

        public ApplicationPatient Get(int id)
        {
            try
            {
                return _unitOfWork.ApplicationPatientRepository.Get(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ApplicationPatientService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<ApplicationPatient> GetAll()
        {
            try
            {
                return _unitOfWork.ApplicationPatientRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ApplicationPatientService in GetAll {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<ApplicationPatient> GetNonHospitalized()
        {
            try
            {
                return _unitOfWork.ApplicationPatientRepository.GetNonHospitalized();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ApplicationPatientService in GetNonHospitalized {e.Message} in {e.StackTrace}");
                return null;
            }
        }
    }
}
