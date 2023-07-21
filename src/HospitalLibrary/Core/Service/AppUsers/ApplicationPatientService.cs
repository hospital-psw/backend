namespace HospitalLibrary.Core.Service.AppUsers
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.AppUsers.Core;
    using MailKit.Net.Smtp;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using MimeKit;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ApplicationPatientService : BaseService<ApplicationPatient>, IApplicationPatientService
    {
        private readonly ILogger<ApplicationPatient> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationPatientService(ILogger<ApplicationPatient> logger, IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager) : base(unitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
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
        public IEnumerable<ApplicationPatient> GetBlocked()
        {
            try
            {
                return _unitOfWork.ApplicationPatientRepository.GetBlocked();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ApplicationPatientService in GetBlocked {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<ApplicationPatient> GetMalicious()
        {
            try
            {
                return _unitOfWork.ApplicationPatientRepository.GetMalicious();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ApplicationPatientService in GetMalicious {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public async Task<ApplicationPatient> BlockPatient(int id)
        {
            try
            {
                ApplicationPatient patient = (ApplicationPatient)await _userManager.FindByIdAsync(id.ToString());

                if (patient == null)
                    return null;

                patient.Blocked = true;
                patient.Strikes = 0;
                var result = await _userManager.UpdateAsync(patient);
                return patient;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ApplicationPatientService in BlockPatient {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public async Task<ApplicationPatient> UnblockPatient(int id)
        {
            try
            {
                ApplicationPatient patient = (ApplicationPatient)await _userManager.FindByIdAsync(id.ToString());

                if (patient == null)
                    return null;

                patient.Blocked = false;
                var result = await _userManager.UpdateAsync(patient);
                return patient;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ApplicationPatientService in UnblockPatient {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public async Task<ApplicationPatient> SetStrikes(int id, int num)
        {
            try
            {
                ApplicationPatient patient = (ApplicationPatient)await _userManager.FindByIdAsync(id.ToString());

                if (patient == null)
                    return null;

                patient.Strikes = num;
                var result = await _userManager.UpdateAsync(patient);
                return patient;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ApplicationPatientService in UnblockPatient {e.Message} in {e.StackTrace}");
                return null;
            }
        }

 
    }
}
