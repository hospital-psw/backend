namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRelocationService
    {
        RelocationRequest Create(RelocationRequest entity);
        void FinishRelocation();
        void RelocateEquipment(RelocationRequest request);
        List<RelocationRequest> GetAllForRoom(int roomId);
        RelocationRequest GetById(int id);
        void Decline(int requestId);
    }
}
