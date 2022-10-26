namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Settings;
    using HospitalLibrary.Core.Repository;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Linq.Expressions;
    using IdentityModel;

    public class DoctorService : BaseService<Doctor>, IDoctorService
    {

        private ILogger<Doctor> _logger;

        public DoctorService(ILogger<Doctor> logger)
        {
            _logger = logger;
        }

        public override Doctor Add(Doctor entity)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                unitOfWork.DoctorRepository.Add(entity);
                unitOfWork.Save();

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
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                unitOfWork.DoctorRepository.Update(entity);
                unitOfWork.Save();

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
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.DoctorRepository.Get(id);
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
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.DoctorRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in DoctorService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }
    }
}