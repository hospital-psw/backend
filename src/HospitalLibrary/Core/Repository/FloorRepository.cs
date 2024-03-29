﻿namespace HospitalLibrary.Core.Repository
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FloorRepository : BaseRepository<Floor>, IFloorRepository
    {
        public FloorRepository(HospitalDbContext context) : base(context) { }

        public override void Update(Floor floor)
        {
            Floor floorFromBase = this.Get(floor.Id);
            floorFromBase.Purpose = floor.Purpose;
            base.Update(floorFromBase);
            HospitalDbContext.SaveChanges();
        }
    }
}
