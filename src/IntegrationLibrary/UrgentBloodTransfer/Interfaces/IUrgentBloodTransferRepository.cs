namespace IntegrationLibrary.UrgentBloodTransfer.Interfaces
{
    using IntegrationLibrary.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUrgentBloodTransferRepository : IRepository<UrgentBloodTransfer>
    {
        UrgentBloodTransfer Get(UrgentBloodTransfer entity);
        void Delete(UrgentBloodTransfer entity);
    }
}
