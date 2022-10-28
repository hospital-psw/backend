namespace HospitalLibrary.Core.DTO
{
    using HospitalLibrary.Core.Model;
    using System;

    public class RoomDTO
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public FloorDTO Floor { get; set; }
        public BuildingDTO Building { get; set; }
        public string Purpose { get; set; }
        public WorkingHours WorkingHours { get; set; }


        public RoomDTO() { }
        public RoomDTO(Room room)
        {
            Id = room.Id;
            Number = room.Number;
            Floor = new FloorDTO(room.Floor);
            Building = new BuildingDTO(room.Building);
            Purpose = room.Purpose;
            //WorkigHoursDTO = new WorkigHoursDTO(room.WorkingHours);
            if (room.WorkingHours != null)
            {
                WorkingHours = new WorkingHours(room.WorkingHours.Start, room.WorkingHours.End);
            }
            else
            {
                WorkingHours = new WorkingHours(new DateTime(), new DateTime());
            }
        }

    }
}
