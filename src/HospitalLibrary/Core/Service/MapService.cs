namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.DTO;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MapService : IMapService
    {
        public IEnumerable<Building> GetBuildings()
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.MapRepository.GetBuildings();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<RoomMap> GetBuildingRooms(int buildingId)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.MapRepository.GetBuildingRooms(buildingId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<RoomMap> GetFloorRooms(int buildingId, int floor)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.MapRepository.GetFloorRooms(buildingId, floor);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
