namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RenovationEventRepository : BaseRepository<RenovationEvent>, IRenovationEventRepository
    {
        private HospitalDbContext _context;
        public RenovationEventRepository(HospitalDbContext context) : base(context)
        {
            _context = context;
        }

        public RenovationEvent GetScheduleEventForAggregate(int aggregeateId)
        {
            return HospitalDbContext.RenovationEvents.FirstOrDefault(x => !x.Deleted && x.AggregateId == aggregeateId && x.EventName == "SCHEDULE_EVENT");
        }

    }
}
