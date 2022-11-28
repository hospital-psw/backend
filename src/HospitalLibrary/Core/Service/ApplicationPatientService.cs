namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
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
        public ApplicationPatient GetPatient(int patientId)
        {
            try
            {
                return _unitOfWork.ApplicationUserRepository.GetPatient(patientId);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ApplicationPatientService in GetPatient {e.Message} in {e.StackTrace}");
                return null;
            }
        }

    }
}
