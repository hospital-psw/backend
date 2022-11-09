namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;


    public class EquipmentMapper
    {
        public static EquipmentDto EntityToEntityDto(EquipmentDto equipment)
        {
            EquipmentDto dto = new EquipmentDto();
            dto.equipmentType = equipment.equipmentType;
            dto.quantity = equipment.quantity;
            return dto;
        }

        public static Equipment EntityDtoToEntity(EquipmentDto dto)
        {
            Equipment equipment = new Equipment();

            equipment.EquipmentType = dto.equipmentType;
            equipment.Quantity = dto.quantity;

            return equipment;
        }
    }
}
