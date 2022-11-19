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
        List<DateTime> GetAvailableAppointments(int roomId1, int roomId2, DateTime from, DateTime to, int duration);
        RelocationRequest Create(RelocationRequest entity);
        void FinishRelocation();

        void RelocateEquipment(RelocationRequest request);
    }
}
