namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.DTO;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Settings;
    using IdentityModel;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public class DoctorService : BaseService<Doctor>, IDoctorService
    {

        private ILogger<Doctor> _logger;

        public DoctorService(ILogger<Doctor> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _logger = logger;
        }

        public override Doctor Add(Doctor entity)
        {
            try
            {
                _unitOfWork.DoctorRepository.Add(entity);
                _unitOfWork.Save();

                return entity;

            }
            catch (Exception e)
            {
                _logger.LogError($"Error in DoctorService in Get {e.Message} in {e.StackTrace}");
                return null;
            }

        }

        public override Doctor Update(Doctor entity)
        {
            try
            {
                _unitOfWork.DoctorRepository.Update(entity);
                _unitOfWork.Save();

                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in DoctorService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override Doctor Get(int id)
        {
            try
            {
                return _unitOfWork.DoctorRepository.Get(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in DoctorService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override IEnumerable<Doctor> GetAll()
        {
            try
            {
                return _unitOfWork.DoctorRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in DoctorService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public IEnumerable<Doctor> GetBySpecialization(Specialization specialization)
        {
            try
            {
                return _unitOfWork.DoctorRepository.GetBySpecialization(specialization);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in DoctorService in GetBySpecialization {e.Message} in {e.StackTrace}");
                return null;
            }
        }
    }
}
