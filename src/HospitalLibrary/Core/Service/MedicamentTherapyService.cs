namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model.Medicament;
    using HospitalLibrary.Core.Model.Therapy;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Repository.Core;
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

        public MedicamentTherapyService(ILogger<MedicamentTherapy> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _logger = logger;
        }

        public override MedicamentTherapy Add(MedicamentTherapy entity)
        {
            try
            {
                Medicament medicament = _unitOfWork.MedicamentRepository.Get(entity.Medicament.Id);
                medicament.Quantity = medicament.Quantity - entity.AmountOfMedicament;
                _unitOfWork.MedicamentRepository.Update(medicament);

                _unitOfWork.MedicamentTherapyRepository.Add(entity);
                _unitOfWork.Save();
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
                _unitOfWork.MedicamentTherapyRepository.Update(entity);
                _unitOfWork.Save();

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
                return _unitOfWork.MedicamentTherapyRepository.Get(id);
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
                return _unitOfWork.MedicamentTherapyRepository.GetAll();
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
                medicamentTherapy.Deleted = true;
                _unitOfWork.MedicamentTherapyRepository.Update(medicamentTherapy);
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicamentTherapyService in Get {e.Message} in {e.StackTrace}");
            }
        }
    }
}
