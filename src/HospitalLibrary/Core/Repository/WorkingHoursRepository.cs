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

    public class WorkingHoursRepository : BaseRepository<WorkingHours>, IWorkingHoursRepository
    {
        public WorkingHoursRepository(HospitalDbContext context) : base(context) { }

        public override void Update(WorkingHours entity)
        {
            if(entity == null)
            {
                return;
            }

            WorkingHours workingHoursFromBase = this.Get(entity.Id);
            workingHoursFromBase.Start = entity.Start;
            workingHoursFromBase.End = entity.End;
            base.Update(workingHoursFromBase);
            HospitalDbContext.SaveChanges();
        }
    }
}
