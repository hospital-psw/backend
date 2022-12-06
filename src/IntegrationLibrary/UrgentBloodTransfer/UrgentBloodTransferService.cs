namespace IntegrationLibrary.UrgentBloodTransfer
{
    using Grpc.Core;
    using grpcServices;
    using IntegrationLibrary.Core;
    using IntegrationLibrary.UrgentBloodTransfer.Interfaces;
    using HospitalLibrary.Core.Repository.Core;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HospitalLibrary.Core.Model.Blood;

    public class UrgentBloodTransferService : IUrgentBloodTransferService
    {
        private Channel _channel;
        private UrgentBloodTransferGrpcService.UrgentBloodTransferGrpcServiceClient _grpcClient;

        private readonly ILogger<UrgentBloodTransfer> _logger;
        private readonly IntegrationLibrary.Core.IUnitOfWork _integrationUnitOfWork;
        private readonly HospitalLibrary.Core.Repository.Core.IUnitOfWork _hospitalUnitOfWork;

        public UrgentBloodTransferService(ILogger<UrgentBloodTransfer> logger, IntegrationLibrary.Core.IUnitOfWork integrationUnitOfWork, HospitalLibrary.Core.Repository.Core.IUnitOfWork hospitalUnitOfWork)
        {
            _logger = logger;
            _integrationUnitOfWork = integrationUnitOfWork;
            _hospitalUnitOfWork = hospitalUnitOfWork;
        }

        public UrgentBloodTransfer Create(UrgentBloodTransfer entity)
        {
            try
            {
                _integrationUnitOfWork.UrgentBloodTransferRepository.Add(entity);
                _integrationUnitOfWork.Save();

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
                return _integrationUnitOfWork.UrgentBloodTransferRepository.GetAll();
            }
            catch(Exception e)
            {
                _logger.LogError($"Error in UrgentBloodTransfer in GetAll {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        private UrgentBloodTransferResponse GetResponse(UrgentBloodTransferRequest request)
        {
            _channel = new Channel("localhost:9090", ChannelCredentials.Insecure);
            _grpcClient = new UrgentBloodTransferGrpcService.UrgentBloodTransferGrpcServiceClient(_channel);

            UrgentBloodTransferResponse response = _grpcClient.transfer(request);
            _channel?.ShutdownAsync();

            return response;
        }

        public bool RequestBlood(UrgentBloodTransferRequest request)
        {
            try
            {
                var response = GetResponse(request);
                
                if (response.HasBlood)
                {

                    _integrationUnitOfWork.UrgentBloodTransferRepository.Add(new UrgentBloodTransfer(request.BloodType, request.Amount));
                    
                    _hospitalUnitOfWork.BloodUnitRepository.Add(new BloodUnit((HospitalLibrary.Core.Model.Blood.Enums.BloodType)request.BloodType,(int)request.Amount));
                    
                    return true;
                }
                else
                    return false;

            }
            catch(Exception e)
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
                return _integrationUnitOfWork.UrgentBloodTransferRepository.Get(entity);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in UrgentBloodTransferService in Get(entity) {e.Message} in {e.StackTrace}");
                return null;
            }
        }
    }
}
