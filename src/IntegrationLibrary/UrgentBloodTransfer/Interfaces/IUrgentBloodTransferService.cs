namespace IntegrationLibrary.UrgentBloodTransfer.Interfaces
{
    using IntegrationLibrary.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUrgentBloodTransferService : IService<UrgentBloodTransfer>
    {
        UrgentBloodTransfer Get(UrgentBloodTransfer entity);
        void RequestBlood();
    }
}
