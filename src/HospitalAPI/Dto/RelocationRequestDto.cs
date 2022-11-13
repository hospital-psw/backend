namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model;
    using System;

    public class RelocationRequestDto
    {
        public RoomDto FromRoom { get; set; }
        public RoomDto ToRoom { get; set; }
        public Equipment Equipment { get; set; }
        public int Quantity { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }

        public RelocationRequestDto(RoomDto fromRoom, RoomDto toRoom, Equipment equipment, int quantity, DateTime startTime, int duration)
        {
            FromRoom = fromRoom;
            ToRoom = toRoom;
            Equipment = equipment;
            Quantity = quantity;
            StartTime = startTime;
            Duration = duration;
        }

        public RelocationRequestDto() { }
    }
}
