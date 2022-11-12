namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Settings;
    using IdentityModel;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PatientService : BaseService<Patient>, IPatientService
    {
        private readonly ILogger<Patient> _logger;

        public PatientService(ILogger<Patient> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _logger = logger;
        }

        public Patient Add(Patient patient)
        {
            try
            {
                _unitOfWork.PatientRepository.Add(patient);
                _unitOfWork.Save();

                return patient;

            }
            catch (Exception e)
            {
                _logger.LogError($"Error in PatientService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public Patient Get(int patientId)
        {
            try
            {
                return _unitOfWork.GetRepository<Patient>().Get(patientId);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in PatientService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override IEnumerable<Patient> GetAll()
        {
            try
            {
                return _unitOfWork.PatientRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in PatientService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override Patient Update(Patient patient)
        {
            try
            {
                _unitOfWork.PatientRepository.Update(patient);
                _unitOfWork.Save();

                return patient;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in AppointmentService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }
    }
}
