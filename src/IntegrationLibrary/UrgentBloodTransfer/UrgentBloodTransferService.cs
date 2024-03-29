﻿namespace IntegrationLibrary.UrgentBloodTransfer
{
    using Grpc.Core;
    using grpcServices;
    using IntegrationLibrary.Core;
    using IntegrationLibrary.UrgentBloodTransfer.Interfaces;
    using IntegrationLibrary.UrgentBloodTransfer.Model;
    using IntegrationLibrary.UrgentBloodTransfer.Senders;
    using IntegrationLibrary.Util;
    using IntegrationLibrary.Util.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UrgentBloodTransferService : IUrgentBloodTransferService
    {
        private readonly ILogger<UrgentBloodTransfer> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBBConnections _connections;
        private readonly IMailSender _mailSender;
        private readonly IConfiguration _configuration;

        public UrgentBloodTransferService(ILogger<UrgentBloodTransfer> logger, IUnitOfWork integrationUnitOfWork, IBBConnections connections, IMailSender mailSender, IConfiguration configuration)
        {
            _logger = logger;
            _unitOfWork = integrationUnitOfWork;
            _connections = connections;
            _mailSender = mailSender;
            _configuration = configuration;
        }

        public UrgentBloodTransfer Create(UrgentBloodTransfer entity)
        {
            try
            {
                _unitOfWork.UrgentBloodTransferRepository.Add(entity);
                _unitOfWork.Save();

                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in UrgentBloodTransferService in Create {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public UrgentBloodTransfer Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UrgentBloodTransfer> GetAll()
        {
            try
            {
                return _unitOfWork.UrgentBloodTransferRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in UrgentBloodTransfer in GetAll {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        private IUrgentBloodTransferSender InstantiateSender(bool Http)
        {
            if (Http)
            {
                return new HTTPUrgentBloodTransferSender();
            }
            else
            {
                return new GRPCUrgentBloodTransferSender();
            }
        }

        public bool RequestBlood(UrgentBloodTransfer request)
        {
            try
            {
                IUrgentBloodTransferSender sender = InstantiateSender(request.HTTP);

                var response = sender.SendUrgentBloodRequest(request);

                if (response)
                {
                    _connections.SendBloodUnitToHospital(new BloodUnit((Model.BloodType)request.BloodType, (int)request.Amount));

                    string templateAccept = MailSender.MakeAcceptBloodUnitTemplate(request.Amount, request.Sender.Name, request.BloodType);
                    _mailSender.SendEmail(templateAccept, "Successful blood transfer", _configuration["ManagerEmail"]);

                    _unitOfWork.UrgentBloodTransferRepository.Add(request);
                    _unitOfWork.Save();

                }
                else
                {
                    string templateDecline = MailSender.MakeDeclineBloodUnitTemplate(request.Amount, request.Sender.Name, request.BloodType);
                    _mailSender.SendEmail(templateDecline, "Unsuccessful blood transfer", _configuration["ManagerEmail"]);
                }
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in UrgentBloodTransferService in RequestBlood {e.Message} in {e.StackTrace}");
                return false;
            }

        }

        public UrgentBloodTransfer Update(UrgentBloodTransfer entity)
        {
            throw new NotImplementedException();
        }

        public UrgentBloodTransfer Get(UrgentBloodTransfer entity)
        {
            try
            {
                return _unitOfWork.UrgentBloodTransferRepository.Get(entity);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in UrgentBloodTransferService in Get(entity) {e.Message} in {e.StackTrace}");
                return null;
            }
        }
    }
}
