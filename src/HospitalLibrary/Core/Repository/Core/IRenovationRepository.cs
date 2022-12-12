namespace HospitalLibrary.Core.Repository.Core
{
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRenovationRepository : IBaseRepository<RenovationRequest>
    {
        RenovationRequest Create(RenovationRequest request);
        List<RenovationRequest> GetScheduledRenovationsForRoom(int roomId);
        List<RenovationRequest> GetFinishedRenovations();

        int Save();
        List<RenovationRequest> GetAll();
    }
}
