namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class WorkingHoursRepository: BaseRepository<WorkingHours>, IWorkingHoursRepository
    {
        public WorkingHoursRepository(HospitalDbContext context) : base(context) { }
    }
}
