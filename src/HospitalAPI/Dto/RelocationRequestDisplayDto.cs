namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model.Enums;
    using System;

    public class RelocationRequestDisplayDto
    {
        public int Id { get; set; }
        public string FromRoomNumber { get; set; }
        public string ToRoomNumber { get; set; }
        public int FromRoomId { get; set; }
        public int ToRoomId { get; set; }
        public EquipmentType EquipmentType { get; set; }
        public int Quantity { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }

        public RelocationRequestDisplayDto(int id, string fromRoomNumber, string toRoomNumber, int fromRoomId, int toRoomId, EquipmentType equipmentType, int quantity, DateTime startTime, int duration)
        {
            Id = id;
            FromRoomNumber = fromRoomNumber;
            ToRoomNumber = toRoomNumber;
            FromRoomId = fromRoomId;
            ToRoomId = toRoomId;
            EquipmentType = equipmentType;
            Quantity = quantity;
            StartTime = startTime;
            Duration = duration;
        }

        public RelocationRequestDisplayDto()
        {
        }
    }
}
