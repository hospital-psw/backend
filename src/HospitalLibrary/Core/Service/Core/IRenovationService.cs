namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.Model;
    using System.Collections.Generic;

    public interface IRenovationService
    {
        RenovationRequest Create(RenovationRequest entity);
        List<RenovationRequest> GetAllForRoom(int roomId);
        void Decline(int requestId);
    }
}
