namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.VacationRequests;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RenovationService : BaseService<RenovationRequest>, IRenovationService
    {
        public RenovationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public RenovationRequest Create(RenovationRequest entity)
        {
            try
            {
                return _unitOfWork.RenovationRepository.Create(entity);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<RenovationRequest> GetAllForRoom(int roomId)
        {
            List<RenovationRequest> futureRenovations = new List<RenovationRequest>();

            foreach (RenovationRequest renovationRequest in _unitOfWork.RenovationRepository.GetAll())
            {
                foreach (Room room in renovationRequest.Rooms)
                {
                    if (room.Id != roomId) continue;
                    if (renovationRequest.StartTime >= DateTime.Now) futureRenovations.Add(renovationRequest);
                }
            }
            return futureRenovations;
        }

        public void Decline(int requestId)
        {
            RenovationRequest request = _unitOfWork.RenovationRepository.Get(requestId);
            request.Deleted = true;
            _unitOfWork.RenovationRepository.Update(request);
            _unitOfWork.RenovationRepository.Save();
        }

        public void FinishRenovation()
        {

            List<RenovationRequest> finishedRenovations = _unitOfWork.RenovationRepository.GetFinishedRenovations();
            Console.WriteLine(finishedRenovations.Count);
            foreach (RenovationRequest req in finishedRenovations)
            {
                if (req.RenovationType == Model.Enums.RenovationType.MERGE)
                    FinishMergeRenovation(req);
                else if (req.RenovationType == Model.Enums.RenovationType.SPLIT)
                    FinishSplitRenovation(req);
                req.Delete();
                _unitOfWork.RenovationRepository.Save();
            }
        }

        public void FinishMergeRenovation(RenovationRequest request)
        {

            Room room1 = request.Rooms[0];
            Room room2 = request.Rooms[1];

            RoomMap roomMap1 = _unitOfWork.MapRepository.GetRoomMapById(room1.Id);
            RoomMap roomMap2 = _unitOfWork.MapRepository.GetRoomMapById(room2.Id);

            Room newRoom = Room.Create(request.RenovationDetails[0].NewRoomName, room1.Floor, request.RenovationDetails[0].NewRoomPurpose, room1.WorkingHours);
            newRoom.SetCapacity(request.RenovationDetails[0].NewCapacity);
            _unitOfWork.RoomRepository.Create(newRoom);
            RoomMap newRoomMap = RoomMap.Create(newRoom, GetNewRoomX(room1, room2), roomMap1.Z, GetNewRoomWidth(room1, room2), roomMap1.depth);
            _unitOfWork.MapRepository.Create(newRoomMap);

            //Move all equipment to new room
            MoveEquipmentToNewRoom(GetEquipmentForMoveInMerge(room1, room2), newRoom);
            FilterEquipmentForMove(newRoom);

            //Delete relocations for old rooms
            DeleteRelocationsAfterRenovation(_unitOfWork.RelocationRepository.GetScheduledRelocationsForRoom(room1.Id));
            DeleteRelocationsAfterRenovation(_unitOfWork.RelocationRepository.GetScheduledRelocationsForRoom(room2.Id));

            //Delete appointments for old rooms
            DeleteAppointmentsAfterRenovation((List<Appointment>)_unitOfWork.AppointmentRepository.GetScheduledAppointmentsForRoom(room1.Id));
            DeleteAppointmentsAfterRenovation((List<Appointment>)_unitOfWork.AppointmentRepository.GetScheduledAppointmentsForRoom(room2.Id));

            //Delete old rooms
            roomMap1.Delete();
            roomMap2.Delete();
            _unitOfWork.MapRepository.Save();
            room1.Delete();
            room2.Delete();
            _unitOfWork.RoomRepository.Save();
        }

        public void FinishSplitRenovation(RenovationRequest request)
        {
            Room roomToBeSplit = request.Rooms[0];
            List<Room> createdRooms = CreateNewRooms(_unitOfWork.MapRepository.GetRoomMapById(roomToBeSplit.Id), request);

            //Create two new rooms
            Room newRoom1 = createdRooms[0];
            Room newRoom2 = createdRooms[1];

            List<RoomMap> createdRoomMaps = CreateNewRoomMap(_unitOfWork.MapRepository.GetRoomMapById(roomToBeSplit.Id), newRoom1, newRoom2);

            RoomMap newRoomMap1 = createdRoomMaps[0];
            RoomMap newRoomMap2 = createdRoomMaps[1];

            //Move equipment to one of new rooms
            MoveEquipmentToNewRoom(_unitOfWork.EquipmentRepository.GetEquipmentForRoom(roomToBeSplit), newRoom1);

            //Delete relocations for old room
            DeleteRelocationsAfterRenovation(_unitOfWork.RelocationRepository.GetScheduledRelocationsForRoom(roomToBeSplit.Id));

            //Delete appointments for old room
            DeleteAppointmentsAfterRenovation((List<Appointment>)_unitOfWork.AppointmentRepository.GetScheduledAppointmentsForRoom(roomToBeSplit.Id));

            //Delete old room
            roomToBeSplit.Delete();
            _unitOfWork.RoomRepository.Save();
        }

        public List<RoomMap> CreateNewRoomMap(RoomMap roomToBeSplit, Room firstRoom, Room secondRoom)
        {
            List<RoomMap> newRoomMaps = new List<RoomMap>();
            RoomMap firstRoomMap;
            RoomMap secondRoomMap;

            if (roomToBeSplit.width == 3)
            {
                firstRoomMap = RoomMap.Create(firstRoom, roomToBeSplit.X - 1, roomToBeSplit.Z, 1, roomToBeSplit.depth);
                secondRoomMap = RoomMap.Create(secondRoom, firstRoomMap.X + firstRoomMap.width + 0.5, roomToBeSplit.Z, 2, roomToBeSplit.depth);
            }
            else
            {
                firstRoomMap = RoomMap.Create(firstRoom, roomToBeSplit.X - 0.5, roomToBeSplit.Z, 1, roomToBeSplit.depth);
                secondRoomMap = RoomMap.Create(secondRoom, firstRoomMap.X + firstRoomMap.width, roomToBeSplit.Z, 1, roomToBeSplit.depth);
            }

            roomToBeSplit.Delete();
            _unitOfWork.MapRepository.Create(firstRoomMap);
            _unitOfWork.MapRepository.Create(secondRoomMap);
            _unitOfWork.MapRepository.Update(roomToBeSplit);
            _unitOfWork.MapRepository.Save();


            newRoomMaps.Add(firstRoomMap);
            newRoomMaps.Add(secondRoomMap);

            return newRoomMaps;
        }

        public List<Room> CreateNewRooms(RoomMap roomToBeSplit, RenovationRequest request)
        {
            List<Room> newRooms = new List<Room>();

            Room firstRoom = Room.Create(request.RenovationDetails[0].NewRoomName, roomToBeSplit.Room.Floor, request.RenovationDetails[0].NewRoomPurpose, null);
            firstRoom.SetCapacity(request.RenovationDetails[0].NewCapacity);
            newRooms.Add(firstRoom);

            Room secondRoom = Room.Create(request.RenovationDetails[1].NewRoomName, roomToBeSplit.Room.Floor, request.RenovationDetails[1].NewRoomPurpose, null);
            secondRoom.SetCapacity(request.RenovationDetails[1].NewCapacity);
            newRooms.Add(secondRoom);
            _unitOfWork.RoomRepository.Create(firstRoom);
            _unitOfWork.RoomRepository.Create(secondRoom);
            _unitOfWork.RoomRepository.Save();

            return newRooms;
        }

        public List<Equipment> GetEquipmentForMoveInMerge(Room room1, Room room2)
        {
            List<Equipment> equipmentToMove = _unitOfWork.EquipmentRepository.GetEquipmentForRoom(room1);
            equipmentToMove.AddRange(_unitOfWork.EquipmentRepository.GetEquipmentForRoom(room2));
            return equipmentToMove;
        }

        public void DeleteRelocationsAfterRenovation(List<RelocationRequest> relocations)
        {
            foreach (RelocationRequest req in relocations)
            {
                req.DeleteRelocation();
                _unitOfWork.RelocationRepository.Update(req);
                _unitOfWork.RelocationRepository.Save();
            }
        }

        public void MoveEquipmentToNewRoom(List<Equipment> equipmentToMove, Room newRoom)
        {
            foreach (Equipment e in equipmentToMove)
            {
                e.MoveEquipment(newRoom);
                _unitOfWork.EquipmentRepository.Update(e);
                _unitOfWork.EquipmentRepository.Save();
            }
        }

        public void FilterEquipmentForMove(Room newRoom)
        {
            List<Equipment> equipment = _unitOfWork.EquipmentRepository.GetEquipmentForRoom(newRoom);
            foreach (Equipment e in equipment)
            {
                if (e.Deleted) continue;
                List<Equipment> sameEquipment = _unitOfWork.EquipmentRepository.GetSameEquipmentInRoom(newRoom, e.EquipmentType);
                if (sameEquipment.Count < 2) continue;
                Equipment firstEquipment = sameEquipment[0];
                Equipment secondEquipment = sameEquipment[1];
                firstEquipment.AddQuantity(secondEquipment.Quantity);
                secondEquipment.SetQuantity(0);
                secondEquipment.Delete();
                _unitOfWork.EquipmentRepository.Update(firstEquipment);
                _unitOfWork.EquipmentRepository.Update(secondEquipment);
                _unitOfWork.EquipmentRepository.Save();
            }
        }

        public void DeleteAppointmentsAfterRenovation(List<Appointment> appointments)
        {
            foreach (Appointment app in appointments)
            {
                app.DeleteAppointment();
                _unitOfWork.AppointmentRepository.Update(app);
                _unitOfWork.AppointmentRepository.Save();
            }
        }

        public double GetNewRoomX(Room room1, Room room2)
        {
            RoomMap firstRoomMap = _unitOfWork.MapRepository.GetRoomMapById(room1.Id);
            RoomMap secondRoomMap = _unitOfWork.MapRepository.GetRoomMapById(room2.Id);
            if (firstRoomMap.X < secondRoomMap.X)
            {
                if (secondRoomMap.width == 2)
                    return firstRoomMap.X + 1;
                return firstRoomMap.X + 0.5;
            }
            else
            {
                if (firstRoomMap.width == 2)
                    return secondRoomMap.X + 1;
                return secondRoomMap.X + 0.5;
            }
        }

        public double GetNewRoomWidth(Room room1, Room room2)
        {
            return _unitOfWork.MapRepository.GetRoomMapById(room1.Id).width + _unitOfWork.MapRepository.GetRoomMapById(room2.Id).width;
        }

        public RenovationRequest GetById(int id)
        {
            try
            {
                return _unitOfWork.RenovationRepository.GetById(id);
            }
            catch
            {
                return null;
            }
        }
    }
}
