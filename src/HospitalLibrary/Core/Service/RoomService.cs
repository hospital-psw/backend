using HospitalLibrary.Core.DTO;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using HospitalLibrary.Core.Repository.Core;
using HospitalLibrary.Core.Service.Core;
using HospitalLibrary.Settings;
using IdentityModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace HospitalLibrary.Core.Service
{
    public class RoomService : IRoomService
    {
        public RoomService() : base() {}

        public Room Add(RoomDTO dto)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                Room room = new Room(dto);
                room.Building = unitOfWork.BuildingRepository.Get(dto.Building.Id);
                room.Floor = unitOfWork.FloorRepository.Get(dto.Floor.Id);
                unitOfWork.RoomRepository.Add(room);
                unitOfWork.Save();
                return room;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<RoomDTO> GetAll()
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                List<Room> rooms = unitOfWork.RoomRepository.GetAll().ToList();
                List<RoomDTO> roomsDTO = new List<RoomDTO>();
                foreach (Room room in rooms)
                {
                    roomsDTO.Add(new RoomDTO(room));
                }
                return roomsDTO;
            }
            catch (Exception e)
            {
                return null;
            }
        }

      
    }
}