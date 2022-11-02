namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.Model;
    using System.Collections.Generic;

    public interface IMapService
    {
        IEnumerable<Building> GetBuildings();
        IEnumerable<RoomMap> GetBuildingRooms(int buildingId);
        IEnumerable<RoomMap> GetFloorRooms(int buildingId, int floor);
    }
}
