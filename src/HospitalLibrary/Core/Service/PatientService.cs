namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository;
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

        public PatientService(ILogger<Patient> logger)
        {
            _logger = logger;
        }

        public Patient Add(Patient patient)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                unitOfWork.PatientRepository.Add(patient);
                unitOfWork.Save();

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
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.PatientRepository.Get(patientId);
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
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.PatientRepository.GetAll();
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
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                unitOfWork.PatientRepository.Update(patient);
                unitOfWork.Save();

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
