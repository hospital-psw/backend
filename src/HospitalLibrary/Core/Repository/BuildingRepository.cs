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

    public class BuildingRepository : BaseRepository<Building>, IBuildingRepository
    {
        public BuildingRepository(HospitalDbContext context) : base(context) { }

        public override void Update(Building building)
        {
            Building buildingFromBase = this.Get(building.Id);
            buildingFromBase.Name = building.Name;
            base.Update(buildingFromBase);
            HospitalDbContext.SaveChanges(); // ovo mozda ne mora
        }
    }
}
