using HospitalLibrary.Core.DTO;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using HospitalLibrary.Core.Repository.Core;
using HospitalLibrary.Core.Service.Core;
using HospitalLibrary.Settings;
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
                room.Building = unitOfWork.BuildingRepository.Get(dto.BuildingId);
                room.Floor = unitOfWork.FloorRepository.Get(dto.FloorId);
                unitOfWork.RoomRepository.Add(room);
                unitOfWork.Save();
                return room;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<BuildingDTO> GetAll()
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                List<BuildingDTO> buildings = new List<BuildingDTO>();
                foreach (Building building in unitOfWork.BuildingRepository.GetAll().ToList())
                {
                    BuildingDTO buildingDTO = new BuildingDTO(building);
                    buildingDTO.Floors = GetBuildingFloors(building.Id);
                    buildings.Add(buildingDTO);
                }
                return buildings;
            } catch (Exception)
            {
                return null;
            }
        }

        private List<FloorDTO> GetBuildingFloors(int buildingId)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                List<FloorDTO> floors = new List<FloorDTO>();
                foreach (Floor floor in unitOfWork.RoomRepository.GetAll().Where(x => x.Building.Id == buildingId).Select(x => x.Floor).Distinct().ToList())
                {
                    FloorDTO floorDTO = new FloorDTO(floor);
                    floorDTO.Rooms = GetFloorRooms(buildingId, floor.Id);
                    floors.Add(floorDTO);
                }
                return floors;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private List<RoomDTO> GetFloorRooms(int buildingId, int floorId)
        {
            try
            {   using UnitOfWork unitOfWork = new(new HospitalDbContext());
                List<RoomDTO> rooms = new List<RoomDTO>();
                foreach (Room room in unitOfWork.RoomRepository.GetAll().Where(x => (x.Building.Id == buildingId) && (x.Floor.Id == floorId)).ToList())
                {
                    rooms.Add(new RoomDTO(room));
                }
                return rooms;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}