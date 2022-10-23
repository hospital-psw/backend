namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.DTO;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Settings;
    using System;

    public class BuildingService : BaseService<Building>, IBuildingService
    {
        public BuildingService() : base() { }
        
        public BuildingDetailsDTO GetBuildingDetails(int buildingId)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                BuildingDetailsDTO building = new BuildingDetailsDTO(unitOfWork.BuildingRepository.Get(buildingId));
                return building;
            }
            catch(Exception)
            {
                return null;
            }
        }
        
    }
}
