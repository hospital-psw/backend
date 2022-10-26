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
        public List<RoomMapDTO> GetAll()
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                List<RoomMap> roomsMap = unitOfWork.MapRepository.GetAll().ToList();
                List<RoomMapDTO> roomsMapDTO = new List<RoomMapDTO>();
                foreach (RoomMap roomMap in roomsMap)
                {
                    roomsMapDTO.Add(new RoomMapDTO(roomMap));
                }
                return roomsMapDTO;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}