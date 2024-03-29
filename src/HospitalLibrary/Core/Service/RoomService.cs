﻿using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using HospitalLibrary.Core.Repository.Core;
using HospitalLibrary.Core.Service.Core;
using HospitalLibrary.Settings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service
{
    public class RoomService : BaseService<Room>, IRoomService
    {

        private readonly ILogger<Room> _logger;
        private readonly IEquipmentService _equipmentService;

        public RoomService(ILogger<Room> logger, IEquipmentService equipmentService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _logger = logger;
            _equipmentService = equipmentService;
        }

        public IEnumerable<Room> GetAll()
        {
            return _unitOfWork.RoomRepository.GetAll();
        }

        public List<Room> Search(string roomNumber, int floorNumber, int buildingId, string purpose, DateTime start, DateTime end, int equipmentType, int quantity)
        {
            List<Room> allRooms = (List<Room>)_unitOfWork.RoomRepository.GetAll();
            List<Room> suitableRooms = new List<Room>();
            List<Room> suitableRoomsWithEquipment = new List<Room>();

            foreach (Room room in allRooms)
            {
                if (room.Floor.Building.Id == buildingId && (floorNumber == -1 || room.Floor.Number.Number == floorNumber) && room.Number.Contains(roomNumber) && room.Purpose.Contains(purpose))
                {
                    if (this.CheckWorkingHours(room, start, end))
                    {
                        if (TimeSpan.Compare(start.TimeOfDay, room.WorkingHours.Start.TimeOfDay) != -1 && TimeSpan.Compare(room.WorkingHours.End.TimeOfDay, end.TimeOfDay) != -1)
                        {
                            suitableRooms.Add(room);
                        }
                    }
                    else
                    {
                        suitableRooms.Add(room);
                    }
                }
            }
            suitableRoomsWithEquipment = _equipmentService.SearchRooms(suitableRooms, equipmentType, quantity);

            return suitableRoomsWithEquipment;
        }

        public Room GetById(int id)
        {
            return _unitOfWork.RoomRepository.GetById(id);
        }

        public void Create(Room room)
        {
            _unitOfWork.RoomRepository.Create(room);
            _unitOfWork.Save();
        }

        public bool Update(Room room)
        {
            if (this.NumberIsUnique(room) && this.WorkingHoursIsValid(room.WorkingHours))
            {
                if (_unitOfWork.RoomRepository.Update(room))
                {
                    _unitOfWork.WorkingHoursRepository.Update(room.WorkingHours);
                    _unitOfWork.Save();
                    return true;
                }
            }
            return false;
        }

        public void Delete(Room room)
        {
            _unitOfWork.RoomRepository.Delete(room);
            _unitOfWork.Save();
        }

        /*private bool NumberStartsWithFloorNumber(Room room)
        {
            if (room.Floor.Number.ToString() == room.Number.Substring(0, 1))
            {
                return true;
            }
            return false;
        }*/

        private bool NumberIsUnique(Room room)
        {
            foreach (Room r in _unitOfWork.RoomRepository.GetAll())
            {
                if (r.Number == room.Number && room.Floor.Id == r.Floor.Id && r.Id != room.Id)
                {
                    return false;
                }
            }
            return true;
        }

        private bool WorkingHoursIsValid(WorkingHours workingHours)
        {
            if (workingHours != null)
            {
                if (workingHours.End <= workingHours.Start)
                {
                    return false;
                }

            }
            return true;
        }

        public IEnumerable<Room> GetAvailable()
        {
            try
            {
                return _unitOfWork.RoomRepository.GetAvailableRooms();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in RoomService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        private bool CheckWorkingHours(Room room, DateTime start, DateTime end)
        {
            if (room.WorkingHours == null || TimeSpan.Compare(start.TimeOfDay, end.TimeOfDay) == 0)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Room> GetRoomsWithWorkingHour(int workHourId)
        {
            try
            {
                return _unitOfWork.RoomRepository.GetRoomsWithWorkingHour(workHourId);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in RoomService in Get {e.Message} in {e.StackTrace}");
                return null;
            }
        }
    }
}
