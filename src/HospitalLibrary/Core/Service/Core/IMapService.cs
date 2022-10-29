namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.Model;
    using System.Collections.Generic;

    public interface IMapService
    {
        IEnumerable<RoomMap> GetBuilding(string building);
        IEnumerable<RoomMap> GetFloor(string building, int floor);
    }
}
