namespace HospitalLibrary.Core.Service.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRoomScheduleService
    {
        List<DateTime> GetAppointments(List<int> roomsId, DateTime from, DateTime to, int duration);
    }
}
