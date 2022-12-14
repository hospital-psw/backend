namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;

    public class RoomMapper
    {
        public static RoomDto EntityToEntityDto(Room room)
        {
            RoomDto dto = new RoomDto();
            if (room == null) return null;

            dto.Id = room.Id;
            dto.Number = room.Number;
            dto.Purpose = room.Purpose;
            dto.WorkingHours = WorkingHoursMapper.EntityToEntityDto(room.WorkingHours);
            dto.Floor = FloorMapper.EntityToEntityDto(room.Floor);

            return dto;
        }

        public static Room EntityDtoToEntity(RoomDto dto)
        {
            Room room = Room.Create(dto.Number, FloorMapper.EntityDtoToEntity(dto.Floor), dto.Purpose, WorkingHoursMapper.EntityDtoToEntity(dto.WorkingHours));
            room.SetId(dto.Id);
            return room;
        }
    }
}
