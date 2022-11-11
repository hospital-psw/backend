namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model.Therapy;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Settings;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BloodUnitTherapyService : BaseService<BloodUnitTherapy>, IBloodUnitTherapyService
    {
        private readonly ILogger<BloodUnitTherapy> _logger;

        public BloodUnitTherapyService(ILogger<BloodUnitTherapy> logger) : base()
        {
            _logger = logger;
        }


        public override BloodUnitTherapy Add(BloodUnitTherapy entity)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                unitOfWork.BloodUnitTherapyRepository.Add(entity);
                unitOfWork.Save();

                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodUnitTherapyService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override BloodUnitTherapy Get(int id)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.BloodUnitTherapyRepository.Get(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodUnitTherapyService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override IEnumerable<BloodUnitTherapy> GetAll()
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.BloodUnitTherapyRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodUnitTherapyService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override BloodUnitTherapy Update(BloodUnitTherapy entity)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                unitOfWork.BloodUnitTherapyRepository.Update(entity);
                unitOfWork.Save();

                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodUnitTherapyService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public void Delete(BloodUnitTherapy bloodUnitTherapy)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                bloodUnitTherapy.Deleted = true;
                unitOfWork.BloodUnitTherapyRepository.Update(bloodUnitTherapy);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodUnitTherapyService in Get {e.Message} in {e.StackTrace}");
            }
        }
    }
}
