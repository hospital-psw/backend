namespace HospitalAPI.Dto
{
    using System;

    public class RelocationRequestDisplayDto
    {
        public int FromRoomNumber { get; set; }
        public int ToRoomNumber { get; set; }
        public int FromRoomId { get; set; }
        public int ToRoomId { get; set; }
        public int EquipmentId { get; set; }
        public int Quantity { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }

        public RelocationRequestDisplayDto(int fromRoomNumber, int toRoomNumber, int fromRoomId, int toRoomId, int equipmentId, int quantity, DateTime startTime, int duration)
        {
            FromRoomNumber = fromRoomNumber;
            ToRoomNumber = toRoomNumber;
            FromRoomId = fromRoomId;
            ToRoomId = toRoomId;
            EquipmentId = equipmentId;
            Quantity = quantity;
            StartTime = startTime;
            Duration = duration;
        }

        public RelocationRequestDisplayDto()
        {
        }
    }
}
