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
        public IEnumerable<RoomMap> GetBuilding(string building)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.MapRepository.GetBuilding(building);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<RoomMap> GetFloor(string building, int floor)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                return unitOfWork.MapRepository.GetFloor(building, floor);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
