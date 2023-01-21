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
            dto.ReservedQuantity = equipment.ReservedQuantity;
            return dto;
        }

        public static Equipment EntityDtoToEntity(EquipmentDto dto)
        {
            Equipment equipment = Equipment.CreateWithId(dto.EquipmentType, dto.Quantity, RoomMapper.EntityDtoToEntity(dto.Room), dto.Id, dto.ReservedQuantity);
            return equipment;
        }
    }
}
