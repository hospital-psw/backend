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

    public class MedicamentTherapyService : BaseService<MedicamentTherapy>, IMedicamentTherapyService
    {

        private readonly ILogger<MedicamentTherapy> _logger;

        public MedicamentTherapyService(ILogger<MedicamentTherapy> logger) : base()
        {
            _logger = logger;
        }

        public override MedicamentTherapy Add(MedicamentTherapy entity)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                unitOfWork.MedicamentTherapyRepository.Add(entity);
                unitOfWork.Save();
                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicamentTherapyService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override MedicamentTherapy Update(MedicamentTherapy entity)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                unitOfWork.MedicamentTherapyRepository.Update(entity);
                unitOfWork.Save();

                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicamentTherapyService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override MedicamentTherapy Get(int id)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.MedicamentTherapyRepository.Get(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicamentTherapyService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override IEnumerable<MedicamentTherapy> GetAll()
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.MedicamentTherapyRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicamentTherapyService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public void Delete(MedicamentTherapy medicamentTherapy)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                medicamentTherapy.Deleted = true;
                unitOfWork.MedicamentTherapyRepository.Update(medicamentTherapy);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicamentTherapyService in Get {e.Message} in {e.StackTrace}");
            }
        }
    }
}
