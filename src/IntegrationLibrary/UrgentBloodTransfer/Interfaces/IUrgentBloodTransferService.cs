namespace IntegrationLibrary.UrgentBloodTransfer.Interfaces
{
    using grpcServices;
    using IntegrationLibrary.Core;
    using IntegrationLibrary.UrgentBloodTransfer.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUrgentBloodTransferService : IService<UrgentBloodTransfer>
    {
        UrgentBloodTransfer Get(UrgentBloodTransfer entity);
        bool RequestBlood(UrgentBloodTransfer request);
    }
}
