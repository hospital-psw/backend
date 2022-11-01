namespace IntegrationLibrary.BloodBank
{
    using IntegrationLibrary.BloodBank.Interfaces;
    using IntegrationLibrary.Core;
    using IntegrationLibrary.Settings;
    using IntegrationLibrary.Util;
    using IntegrationLibrary.Util.Interfaces;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    public class BloodBankService : IBloodBankService
    {
        private readonly ILogger<BloodBank> _logger;
        private readonly IMailSender _mailer;
        public BloodBankService(ILogger<BloodBank> logger, IMailSender mailer)
        {
            _logger = logger;
            _mailer = mailer;
        }

        public BloodBank Get(int id)
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

        public IEnumerable<BloodBank> GetAll()
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

        public BloodBank GetByEmail(string email)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new IntegrationDbContext());
                return unitOfWork.BloodBankRepository.GetByEmail(email);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodBankService in GetByEmail {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public BloodBank Create(BloodBank entity)
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

        public bool Delete(int id)
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

        public BloodBank Update(BloodBank entity)
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

        public BloodBank Register(BloodBank entity)
        {
            try
            {
                string apiKey = SecretGenerator.GenerateAPIKey(entity.Email);
                entity.ApiKey = apiKey;

                string password = SecretGenerator.generateRandomPassword();
                entity.AdminPassword = password;
                entity.IsDummyPassword = true;

                using UnitOfWork unitOfWork = new(new IntegrationDbContext());
                unitOfWork.BloodBankRepository.Add(entity);
                unitOfWork.Save();

                string template = MailSender.MakeRegisterTemplate(entity.Email, entity.ApiKey, entity.AdminPassword);
                _mailer.SendEmail(template, "Successfull Registration", entity.Email);

                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in BloodBankService in Register {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public bool CheckBloodType(int id, string type)
        {
            using UnitOfWork unitOfWork = new(new IntegrationDbContext());
            BloodBank bloodBank = unitOfWork.BloodBankRepository.Get(id);

            return SendHttpRequestToBank(bloodBank, type);
        }

        private bool SendHttpRequestToBank(BloodBank bloodBank, string type)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"http://{bloodBank.ApiUrl}/{bloodBank.GetBloodTypeAvailability}/+{type}");
                client.DefaultRequestHeaders.Add("X-API-KEY", bloodBank.ApiKey);
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                return Convert.ToBoolean(json);
            }
        }
    }
}
