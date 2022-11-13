namespace HospitalLibrary.Core.Service.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRelocationService
    {
        List<DateTime> GetAvailableAppointments(int roomId1, int roomId2, DateTime from, DateTime to, int duration);
    }
}
