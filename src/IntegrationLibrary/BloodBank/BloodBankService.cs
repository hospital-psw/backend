namespace IntegrationLibrary.BloodBank
{
    using IntegrationLibrary.BloodBank.Interfaces;
    using IntegrationLibrary.Core;
    using IntegrationLibrary.Settings;
    using IntegrationLibrary.Util;
    using IntegrationLibrary.Util.Interfaces;
    using Mailjet.Client.Resources.SMS;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;

    public class BloodBankService : IBloodBankService
    {
        private readonly ILogger<BloodBank> _logger;
        private readonly IMailSender _mailer;
        private readonly IBBConnections _connections;
        public BloodBankService(ILogger<BloodBank> logger, IMailSender mailer, IBBConnections connections)
        {
            _logger = logger;
            _mailer = mailer;
            _connections = connections;
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

        public virtual BloodBank Register(BloodBank entity)
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

            return _connections.SendHttpRequestToBank(bloodBank, type);
        }

        public bool CheckBloodAmount(int id, string type, double amount)
        {
            using UnitOfWork unitOfWork = new(new IntegrationDbContext());
            BloodBank bloodBank = unitOfWork.BloodBankRepository.Get(id);

            return SendHttpRequestAmountToBank(bloodBank, type, amount);
        }

        private bool SendHttpRequestAmountToBank(BloodBank bloodBank, string type, double amount)
        {
            using (var client = new HttpClient())
            {
                bloodBank.GetBloodTypeAndAmountAvailability.Replace("!BLOOD_TYPE", type).Replace("!AMOUNT", amount.ToString());
                var endpoint = new Uri($"http://{bloodBank.ApiUrl}/{bloodBank.GetBloodTypeAndAmountAvailability}");
                client.DefaultRequestHeaders.Add("X-API-KEY", bloodBank.ApiKey);
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                return Convert.ToBoolean(json);
            }
        }

    }
}
