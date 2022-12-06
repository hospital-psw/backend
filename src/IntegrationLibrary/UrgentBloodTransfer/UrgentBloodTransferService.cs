namespace IntegrationLibrary.UrgentBloodTransfer
{
    using Grpc.Core;
    using grpcServices;
    using IntegrationLibrary.UrgentBloodTransfer.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UrgentBloodTransferService : IUrgentBloodTransferService
    {
        private Channel _channel;
        private UrgentBloodTransferGrpcService.UrgentBloodTransferGrpcServiceClient _grpcClient;

        public UrgentBloodTransferService() { }

        
        public void RequestBlood()
        {
            _channel = new Channel("localhost:9090", ChannelCredentials.Insecure);
            _grpcClient = new UrgentBloodTransferGrpcService.UrgentBloodTransferGrpcServiceClient(_channel);

            UrgentBloodTransferResponse response = _grpcClient.transfer(new UrgentBloodTransferRequest { BloodType = BloodType.Abplus, Amount = 5 });
            Console.WriteLine(response.HasBlood);

            _channel?.ShutdownAsync();
        }
    }
}
