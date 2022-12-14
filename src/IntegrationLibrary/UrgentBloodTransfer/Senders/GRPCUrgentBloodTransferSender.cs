namespace IntegrationLibrary.UrgentBloodTransfer.Senders
{
    using Grpc.Core;
    using grpcServices;
    using IntegrationLibrary.UrgentBloodTransfer.Interfaces;
    using IntegrationLibrary.UrgentBloodTransfer.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GRPCUrgentBloodTransferSender : IUrgentBloodTransferSender
    {
        private Channel _channel;
        private UrgentBloodTransferGrpcService.UrgentBloodTransferGrpcServiceClient _grpcClient;

        public bool SendUrgentBloodRequest(UrgentBloodTransfer urgentBloodRequest)
        {

            UrgentBloodTransferRequest grpcUrgentBloodRequest = new UrgentBloodTransferRequest { BloodType = urgentBloodRequest.BloodType, Amount = urgentBloodRequest.Amount};

            _channel = new Channel("localhost:9090", ChannelCredentials.Insecure);
            _grpcClient = new UrgentBloodTransferGrpcService.UrgentBloodTransferGrpcServiceClient(_channel);

            UrgentBloodTransferResponse response = _grpcClient.transfer(grpcUrgentBloodRequest);
            _channel?.ShutdownAsync();

            return response.HasBlood;
        }
    }
}
