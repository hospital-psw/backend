namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.Model;
    using System.Collections.Generic;

    public interface IRenovationService
    {
        RenovationRequest Create(RenovationRequest entity);
        void FinishRenovation();

        void FinishMergeRenovation(RenovationRequest req);
        void FinishSplitRenovation(RenovationRequest req);

        List<RoomMap> CreateNewRoomMap(RoomMap roomToBeSplit, Room firstRoom, Room secondRoom);
        List<Room> CreateNewRooms(RoomMap roomToBeSplit, RenovationRequest request);
        List<Equipment> GetEquipmentForMoveInMerge(Room room1, Room room2);
        void DeleteRelocationsAfterRenovation(List<RelocationRequest> relocations);
        void MoveEquipmentToNewRoom(List<Equipment> equipmentToMove, Room newRoom);
        void FilterEquipmentForMove(Room newRoom);
        void DeleteAppointmentsAfterRenovation(List<Appointment> appointments);
        double GetNewRoomX(Room room1, Room room2);
        double GetNewRoomWidth(Room room1, Room room2);

    }
}
