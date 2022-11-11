namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model.Medicament;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Settings;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MedicamentService : BaseService<Medicament>, IMedicamentService
    {
        private readonly ILogger<Medicament> _logger;

        public MedicamentService(ILogger<Medicament> logger) : base()
        {
            _logger = logger;
        }

        public override Medicament Add(Medicament entity)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                unitOfWork.MedicamentRepository.Add(entity);
                unitOfWork.Save();

                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicamentService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override Medicament Get(int id)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.MedicamentRepository.Get(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicamentService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override IEnumerable<Medicament> GetAll()
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.MedicamentRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicamentService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public override Medicament Update(Medicament entity)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                unitOfWork.MedicamentRepository.Update(entity);
                unitOfWork.Save();

                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicamentService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public void Delete(Medicament medicament)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                medicament.Deleted = true;
                unitOfWork.MedicamentRepository.Update(medicament);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicamentService in Get {e.Message} in {e.StackTrace}");
            }
        }
    }
}
