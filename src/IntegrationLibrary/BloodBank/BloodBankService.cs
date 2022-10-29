namespace IntegrationLibrary.BloodBank
{
    using IntegrationLibrary.BloodBank.Interfaces;
    using IntegrationLibrary.Core;
    using IntegrationLibrary.Settings;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;

    public class BloodBankService : IBloodBankService
    {
        private readonly ILogger<BloodBank> _logger;
        public BloodBankService(ILogger<BloodBank> logger)
        {
            _logger = logger;
        }

        public virtual BloodBank Get(int id)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new IntegrationDbContext());
                return unitOfWork.BloodBankRepository.Get(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodBankService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public virtual IEnumerable<BloodBank> GetAll()
        {
            try
            {
                using UnitOfWork unitOfWork = new(new IntegrationDbContext());
                return unitOfWork.BloodBankRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodBankService in GetAll {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public virtual BloodBank Create(BloodBank entity)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new IntegrationDbContext());
                unitOfWork.BloodBankRepository.Add(entity);
                unitOfWork.Save();

                return entity;

            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodBankService in Create {e.Message} in {e.StackTrace}");
                return null;
            }

        }

        public virtual bool Delete(int id)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new IntegrationDbContext());
                var entity = unitOfWork.BloodBankRepository.Get(id);

                entity.Deleted = true;
                unitOfWork.BloodBankRepository.Update(entity);
                unitOfWork.Save();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodBankService in Delete {e.Message} in {e.StackTrace}");
                return false;
            }
        }

        public virtual BloodBank Update(BloodBank entity)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new IntegrationDbContext());
                unitOfWork.BloodBankRepository.Update(entity);
                unitOfWork.Save();

                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodBankService in Update {e.Message} in {e.StackTrace}");
                return null;
            }

        }
    }
}
