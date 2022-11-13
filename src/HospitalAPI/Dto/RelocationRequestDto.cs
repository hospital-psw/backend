namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model;
    using System;

    public class RelocationRequestDto
    {
        public int FromRoomId { get; set; }
        public int ToRoomId { get; set; }
        public int EquipmentId { get; set; }
        public int Quantity { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }

        public RelocationRequestDto(int fromRoomId, int toRoomId, int equipmentId, int quantity, DateTime startTime, int duration)
        {
            FromRoomId = fromRoomId;
            ToRoomId = toRoomId;
            EquipmentId = equipmentId;
            Quantity = quantity;
            StartTime = startTime;
            Duration = duration;
        }

        public RelocationRequestDto() { }
    }
}
