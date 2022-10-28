namespace HospitalLibrary.Core.Repository.Core
{
    using HospitalLibrary.Core.Model;
    using System.Collections.Generic;

    public interface IMapRepository : IBaseRepository<RoomMap>
    {
        IEnumerable<RoomMap> GetBuilding(string building);
        IEnumerable<RoomMap> GetFloor(string building, int floor);
    }
}
