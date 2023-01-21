namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Medicament;
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

    public class MedicamentService : BaseService<Medicament>, IMedicamentService
    {
        private readonly ILogger<Medicament> _logger;

        public MedicamentService(ILogger<Medicament> logger, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _logger = logger;
        }

        public override Medicament Add(Medicament entity)
        {
            try
            {
                _unitOfWork.MedicamentRepository.Add(entity);
                _unitOfWork.Save();

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
                return _unitOfWork.MedicamentRepository.Get(id);
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
                return _unitOfWork.MedicamentRepository.GetAll();
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
                _unitOfWork.MedicamentRepository.Update(entity);
                _unitOfWork.Save();

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
                medicament.Deleted = true;
                _unitOfWork.MedicamentRepository.Update(medicament);
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicamentService in Get {e.Message} in {e.StackTrace}");
            }
        }

        public IEnumerable<Medicament> GetAcceptableMedicaments(int patientId)
        {
            try
            {
                ApplicationPatient patient = _unitOfWork.ApplicationPatientRepository.Get(patientId);
                return _unitOfWork.MedicamentRepository.GetAcceptableMedicaments(patient);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in MedicamentService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }
    }
}
