﻿using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using HospitalLibrary.Core.Repository.Core;
using HospitalLibrary.Core.Service.Core;
using HospitalLibrary.Settings;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;

        }

        public IEnumerable<Room> GetAll()
        {
            return _roomRepository.GetAll();
        }

        public List<Room> Search(string roomNumber, int floorNumber, int buildingId, string purpose, DateTime start, DateTime end)
        {
            List<Room> allRooms = (List<Room>) _roomRepository.GetAll();
            List<Room> suitableRooms = new List<Room>();

            foreach(Room room in allRooms)
            {
                if(room.Floor.Building.Id == buildingId)
                {
                    if(floorNumber == -1 || room.Floor.Number == floorNumber)
                    {
                        if (room.Number.Contains(roomNumber))
                        {
                            if (room.Purpose.Contains(purpose))
                            {
                                if (room.WorkingHours.Start <= start && room.WorkingHours.End >= end)
                                {
                                    suitableRooms.Add(room);
                                }
                            }
                        }
                    }
                }
            }

            return suitableRooms;
        }

        public Room GetById(int id)
        {
            return _roomRepository.GetById(id);
        }

        public void Create(Room room)
        {
            _roomRepository.Create(room);
        }

        public bool Update(Room room)
        {
            if (this.NumberStartsWithFloorNumber(room) && this.NumberIsUnique(room) && this.WorkingHoursIsValid(room.WorkingHours))
            {
                _roomRepository.Update(room);
                using UnitOfWork unitOfWork = new(new HospitalDbContext());
                unitOfWork.WorkingHoursRepository.Update(room.WorkingHours);
                //unitOfWork.BuildingRepository.Update(room.Floor.Building);
                //unitOfWork.FloorRepository.Update(room.Floor);
                return true;
            }
            return false;
        }

        public void Delete(Room room)
        {
            _roomRepository.Delete(room);
        }

        private bool NumberStartsWithFloorNumber(Room room)
        {
            if (room.Floor.Number.ToString() == room.Number.Substring(0, 1))
            {
                return true;
            }
            return false;
        }

        private bool NumberIsUnique(Room room)
        {
            foreach (Room r in _roomRepository.GetAll())
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
    }
}
