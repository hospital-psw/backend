namespace IntegrationLibrary.UrgentBloodTransfer.Interfaces
{
    using IntegrationLibrary.UrgentBloodTransfer.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUrgentBloodTransferSender
    {
        bool SendUrgentBloodRequest(UrgentBloodTransfer urgentBloodRequest);
    }
}
