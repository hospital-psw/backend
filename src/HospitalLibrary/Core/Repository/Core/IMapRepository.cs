namespace HospitalLibrary.Core.Repository.Core
{
    using HospitalLibrary.Core.Model;
    using System.Collections.Generic;

    public interface IMapRepository : IBaseRepository<RoomMap>
    {
        IEnumerable<Building> GetBuildings();
        IEnumerable<RoomMap> GetBuildingRooms(int buildingId);
        IEnumerable<RoomMap> GetFloorRooms(int buildingId, int floor);
        RoomMap GetRoomMapById(int id);
        RoomMap Create(RoomMap roomMap);
        void Save();
    }
}
