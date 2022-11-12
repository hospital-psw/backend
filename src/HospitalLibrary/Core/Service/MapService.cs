namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.DTO;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Repository;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using HospitalLibrary.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MapService : IMapService
    {
        private IUnitOfWork _unitOfWork;

        public MapService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Building> GetBuildings()
        {
            try
            {
                return _unitOfWork.MapRepository.GetBuildings();
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
                return _unitOfWork.MapRepository.GetBuildingRooms(buildingId);
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
                return _unitOfWork.MapRepository.GetFloorRooms(buildingId, floor);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
