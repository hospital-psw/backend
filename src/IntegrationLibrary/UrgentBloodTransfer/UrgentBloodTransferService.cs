namespace IntegrationLibrary.UrgentBloodTransfer
{
    using Grpc.Core;
    using grpcServices;
    using IntegrationLibrary.Core;
    using IntegrationLibrary.UrgentBloodTransfer.Interfaces;
    using IntegrationLibrary.Util.Interfaces;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UrgentBloodTransferService : IUrgentBloodTransferService
    {
        private Channel _channel;
        private UrgentBloodTransferGrpcService.UrgentBloodTransferGrpcServiceClient _grpcClient;

        private readonly ILogger<UrgentBloodTransfer> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBBConnections _connections;

        public UrgentBloodTransferService(ILogger<UrgentBloodTransfer> logger, IUnitOfWork integrationUnitOfWork, IBBConnections connections)
        {
            _logger = logger;
            _unitOfWork = integrationUnitOfWork;
            _connections = connections;
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
                    _unitOfWork.UrgentBloodTransferRepository.Add(new UrgentBloodTransfer(request.BloodType, request.Amount));
                    _unitOfWork.Save();

                    _connections.SendBloodUnitToHospital(new BloodUnit((BloodType)request.BloodType, (int)request.Amount));
                }
                
                return response.HasBlood;
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
