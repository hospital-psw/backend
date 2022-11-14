namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;


    public class EquipmentMapper
    {
        public static EquipmentDto EntityToEntityDto(Equipment equipment)
        {
            EquipmentDto dto = new EquipmentDto();
            dto.EquipmentType = equipment.EquipmentType;
            dto.Quantity = equipment.Quantity;
            dto.Id = equipment.Id;
            dto.Room = RoomMapper.EntityToEntityDto(equipment.Room);
            return dto;
        }

        public static Equipment EntityDtoToEntity(EquipmentDto dto)
        {
            Equipment equipment = new Equipment();

            equipment.EquipmentType = dto.EquipmentType;
            equipment.Quantity = dto.Quantity;
            equipment.Id = dto.Id;
            equipment.Room = RoomMapper.EntityDtoToEntity(dto.Room);
            return equipment;
        }
    }
}
