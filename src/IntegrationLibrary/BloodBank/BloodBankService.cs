namespace IntegrationLibrary.BloodBank
{
    using IntegrationLibrary.BloodBank.Interfaces;
    using IntegrationLibrary.Core;
    using IntegrationLibrary.Util;
    using IntegrationLibrary.Util.Interfaces;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    public class BloodBankService : IBloodBankService
    {
        private readonly ILogger<BloodBank> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailSender _mailer;
        private readonly IBBConnections _connections;
        public BloodBankService(ILogger<BloodBank> logger, IUnitOfWork unitOfWork, IMailSender mailer, IBBConnections connections)
        {
            _logger = logger;
            _mailer = mailer;
            _connections = connections;
            _unitOfWork = unitOfWork;
        }

        public BloodBank Get(int id)
        {
            try
            {
                return _unitOfWork.BloodBankRepository.Get(id);
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
                return _unitOfWork.BloodBankRepository.GetAll();
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
                return _unitOfWork.BloodBankRepository.GetByEmail(email);
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
                _unitOfWork.BloodBankRepository.Add(entity);
                _unitOfWork.Save();

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
                var entity = _unitOfWork.BloodBankRepository.Get(id);
                entity.Deleted = true;
                _unitOfWork.BloodBankRepository.Update(entity);
                _unitOfWork.Save();

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
                _unitOfWork.BloodBankRepository.Update(entity);
                _unitOfWork.Save();

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

                _unitOfWork.BloodBankRepository.Add(entity);
                _unitOfWork.Save();

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
            BloodBank bloodBank = _unitOfWork.BloodBankRepository.Get(id);

            return _connections.SendHttpRequestToBank(bloodBank, type);
        }

        public bool CheckBloodAmount(int id, string type, double amount)
        {
            BloodBank bloodBank = _unitOfWork.BloodBankRepository.Get(id);

            return SendHttpRequestAmountToBank(bloodBank, type, amount);
        }

        private bool SendHttpRequestAmountToBank(BloodBank bloodBank, string type, double amount)
        {
            using (var client = new HttpClient())
            {
                var endpointUrl = bloodBank.GetBloodTypeAndAmountAvailability.Replace("!BLOOD_TYPE", type).Replace("!AMOUNT", amount.ToString());
                var endpoint = new Uri($"http://{bloodBank.ApiUrl}/{endpointUrl}");
                client.DefaultRequestHeaders.Add("X-API-KEY", bloodBank.ApiKey);
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                return Convert.ToBoolean(json);
            }
        }

        public BloodBank SaveConfiguration(int id, int frequntly, DateTime reportFrom, DateTime reportTo)
        {
            var bloodBank = _unitOfWork.BloodBankRepository.Get(id);
            bloodBank.Frequently = frequntly;
            bloodBank.ReportFrom = reportFrom;
            bloodBank.ReportTo = reportTo;
            _unitOfWork.Save();
            return bloodBank;
        }

        public BloodBank SaveMonthlyTransferConfiguration(int id, MonthlyTransfer mt)
        {
            var bloodBank = _unitOfWork.BloodBankRepository.Get(id);
            bloodBank.MonthlyTransfer = mt;
            _unitOfWork.Save();
            return bloodBank;
        }
    }
}
