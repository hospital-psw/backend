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
            Equipment equipment = new Equipment(dto.EquipmentType, dto.Quantity, RoomMapper.EntityDtoToEntity(dto.Room), dto.Id, dto.ReservedQuantity);

            //equipment.EquipmentType = dto.EquipmentType;
            //equipment.Quantity = dto.Quantity;
           // equipment.Id = dto.Id;
            //equipment.Room = RoomMapper.EntityDtoToEntity(dto.Room);
            //equipment.ReservedQuantity = dto.ReservedQuantity;
            return equipment;
        }
    }
}
