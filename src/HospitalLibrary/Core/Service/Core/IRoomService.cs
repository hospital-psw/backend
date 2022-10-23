using HospitalLibrary.Core.DTO;
using HospitalLibrary.Core.Model;
using IdentityModel;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service.Core
{
    public interface IRoomService
    {
        List<BuildingDTO> GetAll();
        Room Add(RoomDTO dto);
        RoomDetailsDTO GetDetails(int id);
    }
}