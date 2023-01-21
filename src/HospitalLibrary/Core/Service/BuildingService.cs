namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BuildingService : BaseService<Building>, IBuildingService
    {
        public BuildingService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
